namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class QRCodeMemberSignInHandler :
        IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, ResultCreateMemberQRCode>,
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IRequestHandler<GetMemberCreditAndStatusRequest, CreditAndStatusModel>,
        IRequestHandler<ShowQRCodeRequest, ShowQRCodeViewModel>,
        IRequestHandler<GenerateQRCodeRequest, QRCodeLinkViewModel>,
        IRequestHandler<CheckExistedCustomerRequest, bool>
    {
        private readonly QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper;
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;
        private readonly PasswordService passwordService;

        public QRCodeMemberSignInHandler(
            QrCodeAdapter qrCodeAdapter,
            QRCodeMemberSignInService qrCodeMemberSignInService,
            PasswordService passwordService,
            QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper)
        {
            this.qrCodeAdapter = qrCodeAdapter;
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
            this.passwordService = passwordService;
            this.qrCodeLoginConfigsHelper = qrCodeLoginConfigsHelper;
        }

        public QRCodeMemberSignInViewModel Handle(QRCodeMemberSignInRequest message)
            => new QRCodeMemberSignInViewModel(
               message.MemberName,
               message.CustName,
               message.Credit,
               qrCodeMemberSignInService.GetSuggestUsername(message.CustName, message.CustId),
               message.ShowPageTitle,
               message.ErrorMessage);

        public ResultCreateMemberQRCode Handle(CreateAccountByQRCodeRequest message)
        {
            var creditAndStatusModel = qrCodeMemberSignInService.GetCreditAndStatus(message.UsernameCopyFrom);

            if(creditAndStatusModel.Status != null &&
              !creditAndStatusModel.Status.Equals("OPEN", StringComparison.OrdinalIgnoreCase))
            {
                return new ResultCreateMemberQRCode
                {
                    Success = false,
                    ErrCode = 100,
                    ErrMsg = Resources.CannotCreateClosedMemberQRCode
                };
            }

            var model = qrCodeMemberSignInService.CheckBelongingByUsername(message.CustId, message.UsernameCopyFrom, message.SubAccId);
            var custIdCopyFrom = model.CustId;

            if (custIdCopyFrom > 0 &&
                Enumeration.FromValue<UserLevel>((byte)model.RoleId).IsMemberLevel() &&
                qrCodeMemberSignInService.IsEnableCustomer(custIdCopyFrom))
            {
                var setting = qrCodeMemberSignInService.GetExistedMemberSetting(custIdCopyFrom);

                var passwordModel = passwordService.CreateMemberPasswordRandom();

                var result = qrCodeMemberSignInService.CreateMemberByQRCode(
                    new InsertMemberCommand(
                        setting,
                        message.Username,
                        message.FirstName ?? string.Empty,
                        message.CustId,
                        custIdCopyFrom,
                        message.IP,
                        message.Language,
                        message.SubAccId,
                        message.Credit,
                        passwordModel.EncodePassword,
                        message.UsernameCopyFrom,
                        message.CreditMax));

                result.CustIdCopyFrom = custIdCopyFrom;

                if (result.Success)
                {
                    result.RawPassword = passwordModel.RawPassword;
                }

                return result;
            }

            return new ResultCreateMemberQRCode
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = Resources.UsernameIsInvalid
            };
        }

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
            => qrCodeMemberSignInService.GetUsernamesSuggestion(
                message.CustId,
                message.SuggestionQuery,
                message.IsInternal);

        public CreditAndStatusModel Handle(GetMemberCreditAndStatusRequest message)
            => IsValidBelongingByUsername(
                message.CustId, 
                message.SearchAccountName, 
                message.SubAccId) ?
                qrCodeMemberSignInService.GetCreditAndStatus(message.SearchAccountName) :
                new CreditAndStatusModel();

        public ShowQRCodeViewModel Handle(ShowQRCodeRequest message)
        {
            if (qrCodeMemberSignInService.IsNotFirstLoginByQRCode(message.SearchAccountId))
            {
                var rawPassword = message.RawPassword;

                if (!message.CreateMode)
                {
                    var passwordModel = passwordService.CreateMemberPasswordRandom();
                    rawPassword = passwordModel.RawPassword;

                    qrCodeMemberSignInService.ResetMemberPassword(new ResetMainAccountPasswordCommand
                    {
                        CustId = message.SearchAccountId,
                        UserPwd = passwordModel.EncodePassword,
                        UserId = message.CustId,
                        DisCasino = false,
                        Language = message.Language,
                        SubAccId = message.SubAccId,
                        LoginIP = message.IP
                    });
                }

                var config = qrCodeLoginConfigsHelper.GetQRCodeMemberSiteLoginConfigs();
                var link = qrCodeAdapter.CreateMobileLoginUrl(
                         qrCodeLoginConfigsHelper.LocalTest(),
                        config.Domains[0],
                        config.SiteId,
                        message.SearchAccountName,
                        config.ExpireSeconds);

                var rngCasinoStatusModel = qrCodeMemberSignInService.GetRnGStatus(message.SearchAccountName);

                return new ShowQRCodeViewModel(
                    message.Credit,
                    message.CreditMin,
                    message.CreditMax,
                    message.SearchAccountName,
                    message.SearchAccountId,
                    message.FirstName,
                    link,
                    qrCodeAdapter.Generate(link),
                    config.Domains,
                    message.CreateMode,
                    message.ShowPageTitle,
                    rngCasinoStatusModel.SyncCS,
                    message.CustIdCopyFrom,
                    rawPassword);
            }

            return null;
        }

        public QRCodeLinkViewModel Handle(GenerateQRCodeRequest message)
        {
            if (qrCodeMemberSignInService.IsNotFirstLoginByQRCode(message.SearchAccountId))
            {
                var config = qrCodeLoginConfigsHelper.GetQRCodeMemberSiteLoginConfigs();

                var link = qrCodeAdapter.CreateMobileLoginUrl(
                    qrCodeLoginConfigsHelper.LocalTest(),
                    message.Domain,
                    config.SiteId,
                    message.SearchAccountName,
                    config.ExpireSeconds);

                return new QRCodeLinkViewModel(link, qrCodeAdapter.Generate(link));
            }

            return new QRCodeLinkViewModel();
        }

        public bool Handle(CheckExistedCustomerRequest message)
            => IsValidBelongingByUsername(
                message.CustId, 
                message.Username, 
                message.SubAccId) &&
            qrCodeMemberSignInService.CheckExistedCustomer(message.Username);

        internal bool IsValidBelongingByUsername(int custId, string username, int subAccountId)
        {
            var model = qrCodeMemberSignInService.CheckBelongingByUsername(custId, username, subAccountId);

            return model.CustId > 0 && 
                Enumeration.FromValue<UserLevel>((byte)model.RoleId).IsMemberLevel();
        }
    }
}
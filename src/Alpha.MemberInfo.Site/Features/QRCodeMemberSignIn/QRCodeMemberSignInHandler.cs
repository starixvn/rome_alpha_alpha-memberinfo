namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using System;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Features.Shared.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Products.Core.Services;
    using MediatR;

    public class QRCodeMemberSignInHandler :
        IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IAsyncRequestHandler<CreateAccountByQRCodeRequest, ResultCreateMemberQRCode>,
        IRequestHandler<GetMemberCreditAndStatusRequest, CreditAndStatusModel>,
        IRequestHandler<ShowQRCodeRequest, ShowQRCodeViewModel>,
        IRequestHandler<GenerateQRCodeRequest, QRCodeLinkViewModel>,
        IRequestHandler<CheckExistedCustomerRequest, bool>
    {
        private readonly QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper;
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;
        private readonly PasswordService passwordService;
        private readonly CheckBelongingService checkBelongingService;

        public QRCodeMemberSignInHandler(
           QrCodeAdapter qrCodeAdapter,
           QRCodeMemberSignInService qrCodeMemberSignInService,
           PasswordService passwordService,
           CheckBelongingService checkBelongingService)
            : this(qrCodeAdapter, qrCodeMemberSignInService, passwordService, checkBelongingService, null)
        {
        }

        internal QRCodeMemberSignInHandler(
            QrCodeAdapter qrCodeAdapter,
            QRCodeMemberSignInService qrCodeMemberSignInService,
            PasswordService passwordService,
            CheckBelongingService checkBelongingService,
            QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper)
        {
            this.qrCodeAdapter = qrCodeAdapter;
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
            this.passwordService = passwordService;
            this.checkBelongingService = checkBelongingService;
            this.qrCodeLoginConfigsHelper = qrCodeLoginConfigsHelper ??
                    SimpleServiceLocator.GetService<QRCodeLoginConfigsHelper>();
        }

        public QRCodeMemberSignInViewModel Handle(QRCodeMemberSignInRequest message)
        {
            var onlyShowErrorMessage = false;

            if (string.IsNullOrWhiteSpace(message.ErrorMessage))
            {
                var limitNewDownlineResult = qrCodeMemberSignInService.CheckLimitNewDownline(
                    message.ViewerId,
                    message.ViewerLevelId,
                    message.Language);
                onlyShowErrorMessage = limitNewDownlineResult.IsFailed;
                message.ErrorMessage = limitNewDownlineResult.Message;
            }

            return new QRCodeMemberSignInViewModel(
                   message.MemberName,
                   message.CustName,
                   message.Credit,
                   qrCodeMemberSignInService.GetSuggestUsername(message.CustName, message.CustId),
                   message.ShowPageTitle,
                   message.ErrorMessage,
                   onlyShowErrorMessage);
        }

        public async Task<ResultCreateMemberQRCode> Handle(CreateAccountByQRCodeRequest message)
        {
            var creditAndStatusModel = qrCodeMemberSignInService.GetCreditAndStatus(message.UsernameCopyFrom);

            if (creditAndStatusModel.Status != null &&
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
                var passwordModel = passwordService.CreateMemberPasswordRandom();

                var result = await qrCodeMemberSignInService.CreateMemberByQRCode(
                    new InsertMemberCommand(
                        message.Username,
                        message.FirstName ?? string.Empty,
                        message.CustId,
                        custIdCopyFrom,
                        message.IP,
                        message.Language,
                        message.SubAccId,
                        message.Credit,
                        passwordModel.RawPassword,
                        message.UsernameCopyFrom,
                        message.CreditMax,
                        message.SubUsername,
                        message.SiteDBGroupIp)).ConfigureAwait(false);

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

        public CreditAndStatusModel Handle(GetMemberCreditAndStatusRequest message)
            => IsValidBelongingByUsername(
                message.CustId,
                message.SearchAccountName,
                message.SubAccId) ?
                qrCodeMemberSignInService.GetCreditAndStatus(message.SearchAccountName) :
                new CreditAndStatusModel();

        public ShowQRCodeViewModel Handle(ShowQRCodeRequest message)
        {
            var belongingInfo = checkBelongingService.GetBelongingInfo(
                new CheckBelongingParam(message.ViewerId, message.SearchAccountId, message.SubAccId));

            if (belongingInfo.IsValid(message.ViewerLevelId)
                && qrCodeMemberSignInService.IsNotFirstLoginByQRCode(message.SearchAccountId))
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
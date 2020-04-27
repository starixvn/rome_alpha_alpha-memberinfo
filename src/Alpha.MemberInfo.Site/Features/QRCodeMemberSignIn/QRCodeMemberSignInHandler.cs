namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Products.Core.Services;
    using MediatR;
    using System.Collections.Generic;

    public class QRCodeMemberSignInHandler :
        IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, ResultCreateMemberQRCode>,
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IRequestHandler<GetMemberCreditRequest, CreditRangeModel>,
        IRequestHandler<ShowQRCodeRequest, ShowQRCodeViewModel>,
        IRequestHandler<GenerateQRCodeRequest, string>
    {
        private readonly QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper;
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;

        public QRCodeMemberSignInHandler(
           QrCodeAdapter qrCodeAdapter,
           QRCodeMemberSignInService qrCodeMemberSignInService)
            : this(qrCodeAdapter, qrCodeMemberSignInService, null)
        {
        }

        internal QRCodeMemberSignInHandler(
            QrCodeAdapter qrCodeAdapter,
            QRCodeMemberSignInService qrCodeMemberSignInService,
            QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper)
        {
            this.qrCodeAdapter = qrCodeAdapter;
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
            this.qrCodeLoginConfigsHelper = qrCodeLoginConfigsHelper ??
                    SimpleServiceLocator.GetService<QRCodeLoginConfigsHelper>();
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
            var checkBelongingByUsernameModel = qrCodeMemberSignInService.CheckBelongingByUsername(message.CustId, message.UsernameCopyFrom, message.SubAccId);
            var custIdCopyFrom = checkBelongingByUsernameModel.CustId;

            if (custIdCopyFrom > 0 && qrCodeMemberSignInService.IsEnableCustomer(custIdCopyFrom))
            {
                var setting = qrCodeMemberSignInService.GetExistedMemberSetting(custIdCopyFrom);

                var command = new InsertMemberCommand(
                    setting, 
                    message.Username,
                    message.FirstName ?? string.Empty,
                    message.CustId,
                    custIdCopyFrom,
                    message.IP,
                    message.Language,
                    message.SubAccId,
                    message.Credit);

                var result = qrCodeMemberSignInService.CreateMemberByQRCode(command);
                result.CustIdCopyFrom = custIdCopyFrom;

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
                message.ActorId,
                message.SuggestionQuery,
                message.IsInternal);

        public CreditRangeModel Handle(GetMemberCreditRequest message)
            => qrCodeMemberSignInService.GetCreditRange(message.SearchAccountName);

        public ShowQRCodeViewModel Handle(ShowQRCodeRequest message)
        {
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
                message.MemberNameCopyFrom,
                message.SearchAccountName,
                message.SearchAccountId,
                message.FirstName,
                qrCodeAdapter.Generate(link),
                link,
                config.Domains,
                message.CreateMode,
                message.ShowPageTitle,
                rngCasinoStatusModel.SyncCS,
                message.MemberIdCopyFrom);
        }

        public string Handle(GenerateQRCodeRequest message)
        {
            var config = qrCodeLoginConfigsHelper.GetQRCodeMemberSiteLoginConfigs();

            var link = qrCodeAdapter.CreateMobileLoginUrl(
                qrCodeLoginConfigsHelper.LocalTest(),
                message.Domain,
                config.SiteId,
                message.SearchAccountName,
                config.ExpireSeconds);

            return qrCodeAdapter.Generate(link);
        }
    }
}
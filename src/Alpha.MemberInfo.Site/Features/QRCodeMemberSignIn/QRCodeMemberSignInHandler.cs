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

                var command = new InsertMemberCommand
                {
                    Username = message.Username,
                    CopyFromCustId = custIdCopyFrom,
                    FirstName = message.FirstName ?? string.Empty,
                    ActorId = message.CustId,
                    PlayerDiscount = setting.PlayerDiscount,
                    PlayerDiscountCS = setting.PlayerDiscountCS,
                    PlayerDiscount1x2 = setting.PlayerDiscount1x2,
                    MinBet = setting.MinBet,
                    MaxBet = setting.MaxBet,
                    MaxPerMatch = setting.MaxPerMatch,
                    MinBetV2 = setting.MinBetV2,
                    MaxBetV2 = setting.MaxBetV2,
                    MaxPerMatchV2 = setting.MaxPerMatchV2,
                    MinBetV3 = setting.MinBetV3,
                    MaxBetV3 = setting.MaxBetV3,
                    MaxPerMatchV3 = setting.MaxPerMatchV3,
                    MinBetV5 = setting.MinBetV5,
                    MaxBetV5 = setting.MaxBetV5,
                    MaxPerMatchV5 = setting.MaxPerMatchV5,
                    MinBetV8 = setting.MinBetV8,
                    MaxBetV8 = setting.MaxBetV8,
                    MaxPerMatchV8 = setting.MaxPerMatchV8,
                    MinBetV10 = setting.MinBetV10,
                    MaxBetV10 = setting.MaxBetV10,
                    MaxPerMatchV10 = setting.MaxPerMatchV10,
                    MinBetV11 = setting.MinBetV11,
                    MaxBetV11 = setting.MaxBetV11,
                    MaxPerMatchV11 = setting.MaxPerMatchV11,
                    MinBetV99 = setting.MinBetV99,
                    MaxBetV99 = setting.MaxBetV99,
                    MaxPerMatchV99 = setting.MaxPerMatchV99,
                    MinBetV99MP = setting.MinBetV99MP,
                    MaxBetV99MP = setting.MaxBetV99MP,
                    MaxPerMatchV99MP = setting.MaxPerMatchV99MP,
                    MinBetV1MP = setting.MinBetV1MP,
                    MaxBetV1MP = setting.MaxBetV1MP,
                    MaxPerMatchV1MP = setting.MaxPerMatchV1MP,
                    MinBetV161 = setting.MinBetV161,
                    MaxBetV161 = setting.MaxBetV161,
                    MaxPerMatchV161 = setting.MaxPerMatchV161,
                    MaxPerBallV161 = setting.MaxPerBallV161,
                    MinBetV164 = setting.MinBetV164,
                    MaxBetV164 = setting.MaxBetV164,
                    MaxPerMatchV164 = setting.MaxPerMatchv164,

                    Credit = message.Credit,
                    DisableCasino = setting.DisableCasino,
                    DisableNumber = setting.DisableNumber,
                    Recommend = message.CustId,
                    UGroup = setting.UGroup,
                    Closed = setting.Closed,

                    APositionTaking_1 = setting.APositionTaking_1,
                    APositionTaking_2 = setting.APositionTaking_2,
                    APositionTaking_3 = setting.APositionTaking_3,
                    APositionTaking_4 = setting.APositionTaking_4,
                    APositionTaking_5 = setting.APositionTaking_5,
                    APositionTaking_6 = setting.APositionTaking_6,
                    APositionTaking_7 = setting.APositionTaking_7,
                    APositionTaking_8 = setting.APositionTaking_8,
                    APositionTaking_9 = setting.APositionTaking_9,
                    APositionTaking_10 = setting.APositionTaking_10,

                    V2APositionTaking_10 = setting.V2APositionTaking_10,
                    V3APositionTaking_10 = setting.V3APositionTaking_10,
                    V5APositionTaking_10 = setting.V5APositionTaking_10,
                    V8APositionTaking_10 = setting.V8APositionTaking_10,
                    V10APositionTaking_10 = setting.V10APositionTaking_10,
                    V11APositionTaking_10 = setting.V11APositionTaking_10,
                    V99APositionTaking_10 = setting.V99APositionTaking_10,

                    APositionTaking_9999 = setting.APositionTaking_9999,

                    ALivePositionTaking_1 = setting.ALivePositionTaking_1,
                    ALivePositionTaking_3 = setting.ALivePositionTaking_3,
                    ALivePositionTaking_7 = setting.ALivePositionTaking_7,
                    ALivePositionTaking_8 = setting.ALivePositionTaking_8,

                    V2APositionTaking_1 = setting.V2APositionTaking_1,
                    V2APositionTaking_2 = setting.V2APositionTaking_2,
                    V2APositionTaking_3 = setting.V2APositionTaking_3,
                    V2APositionTaking_7 = setting.V2APositionTaking_7,
                    V2APositionTaking_8 = setting.V2APositionTaking_8,
                    V2APositionTaking_20 = setting.V2APositionTaking_20,
                    V2APositionTaking_9999 = setting.V2APositionTaking_9999,

                    V3APositionTaking_1 = setting.V3APositionTaking_1,
                    V3APositionTaking_2 = setting.V3APositionTaking_2,
                    V3APositionTaking_3 = setting.V3APositionTaking_3,
                    V3APositionTaking_20 = setting.V3APositionTaking_20,

                    V5APositionTaking_1 = setting.V5APositionTaking_1,
                    V5APositionTaking_2 = setting.V5APositionTaking_2,
                    V5APositionTaking_3 = setting.V5APositionTaking_3,
                    V5APositionTaking_20 = setting.V5APositionTaking_20,
                    V5APositionTaking_9999 = setting.V5APositionTaking_9999,

                    V8APositionTaking_1 = setting.V8APositionTaking_1,
                    V8APositionTaking_3 = setting.V8APositionTaking_3,
                    V8APositionTaking_20 = setting.V8APositionTaking_20,

                    V10APositionTaking_1 = setting.V10APositionTaking_1,
                    V10APositionTaking_2 = setting.V10APositionTaking_2,
                    V10APositionTaking_3 = setting.V10APositionTaking_3,
                    V10APositionTaking_20 = setting.V10APositionTaking_20,

                    V11APositionTaking_1 = setting.V11APositionTaking_1,
                    V11APositionTaking_20 = setting.V11APositionTaking_20,

                    V99APositionTaking_1 = setting.V99APositionTaking_1,
                    V99APositionTaking_2 = setting.V99APositionTaking_2,
                    V99APositionTaking_3 = setting.V99APositionTaking_3,
                    V99APositionTaking_9 = setting.V99APositionTaking_9,
                    V99APositionTaking_20 = setting.V99APositionTaking_20,
                    V99APositionTaking_9999 = setting.V99APositionTaking_9999,

                    V161APositionTaking_81 = setting.V161APositionTaking_81,
                    V161APositionTaking_83 = setting.V161APositionTaking_83,
                    V161APositionTaking_86 = setting.V161APositionTaking_86,
                    V161APositionTaking_88 = setting.V161APositionTaking_88,

                    V161ALivePositionTaking_85 = setting.V161ALivePositionTaking_85,
                    V161ALivePositionTaking_86 = setting.V161ALivePositionTaking_86,
                    V161ALivePositionTaking_87 = setting.V161ALivePositionTaking_87,
                    V161ALivePositionTaking_89 = setting.V161ALivePositionTaking_89,
                    V161ALivePositionTaking_90 = setting.V161ALivePositionTaking_90,

                    IP = message.IP,
                    SystemId = 2,
                    Language = message.Language,
                    SubAccId = message.SubAccId,
                    IsSearchAndCopy = true
                };

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
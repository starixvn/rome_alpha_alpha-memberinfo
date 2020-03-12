namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Web.Models;
    using MediatR;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class QRCodeMemberSignInHandler : 
        IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, ResultCreateMemberQRCode>,
        IRequestHandler<UserNameSuggestionQuery, IEnumerable<UsernameSuggestion>>,
        IRequestHandler<GetMemberCreditRequest, CreditRangeModel>,
        IRequestHandler<ShowQRCodeRequest, ShowQRCodeViewModel>
    {
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;

        public QRCodeMemberSignInHandler(
            QrCodeAdapter qrCodeAdapter, 
            QRCodeMemberSignInService qrCodeMemberSignInService)
        {
            this.qrCodeAdapter = qrCodeAdapter;
            this.qrCodeMemberSignInService = qrCodeMemberSignInService;
        }

        public QRCodeMemberSignInViewModel Handle(QRCodeMemberSignInRequest message)
        { 
            return new QRCodeMemberSignInViewModel(
                message.MemberName, 
                message.ActorName,
                message.Credit.Equals(0) ? 1000 : message.Credit, 
                message.ErrorMessage);
        }

        public ResultCreateMemberQRCode Handle(CreateAccountByQRCodeRequest message)
        {
            var setting = qrCodeMemberSignInService.GetExistedMemberSetting(41961033);

            var command = new InsertMemberCommand
            {
                Username = message.Username,
                FirstName = message.FirstName ?? string.Empty,
                ActorId = message.ActorId,
                PlayerDiscount = setting.Playerdiscount,
                PlayerDiscountCS = setting.Playerdiscountcs,
                PlayerDiscount1x2 = setting.Playerdiscount1x2,
                MinBet = setting.Minbet,
                MaxBet = setting.Maxbet,
                MaxPerMatch = setting.Maxpermatch,
                MinBetV2 = setting.Minbetv2,
                MaxBetV2 = setting.Maxbetv2,
                MaxPerMatchV2 = setting.Maxpermatchv2,
                MinBetV3 = setting.Minbetv3,
                MaxBetV3 = setting.Maxbetv3,
                MinBetV5 = setting.Minbetv5,
                MaxBetV5 = setting.Maxbetv5,
                MaxPerMatchV5 = setting.Maxpermatchv5,
                MinBetV8 = setting.Minbetv8,
                MaxBetV8 = setting.Maxbetv8,
                MaxPerMatchV8 = setting.Maxpermatchv8,
                MinBetV10 = setting.Minbetv10,
                MaxBetV10 = setting.Maxbetv10,
                MaxPerMatchV10 = setting.Maxpermatchv10,
                MinBetV11 = setting.Minbetv11,
                MaxBetV11 = setting.Maxbetv11,
                MaxPerMatchV11 = setting.Maxpermatchv11,
                MinBetV99 = setting.Minbetv99,
                MaxBetV99 = setting.Maxbetv99,
                MaxPerMatchV99 = setting.Maxpermatchv99,
                MinBetV99MP = setting.Minbetv99MP,
                MaxBetV99MP = setting.Maxbetv99MP,
                MaxPerMatchV99MP = setting.Maxpermatchv99MP,
                MinBetV1MP = setting.Minbetv1MP,
                MaxBetV1MP = setting.Maxbetv1MP,
                MaxPerMatchV1MP = setting.Maxpermatchv1MP,
                MinBetV161 = setting.Minbetv161,
                MaxBetV161 = setting.Maxbetv161,
                MaxPerMatchV161 = setting.Maxpermatchv161,
                MaxPerBallV161 = setting.Maxperballv161,
                MinBetV164 = setting.Minbetv164,
                MaxBetV164 = setting.Maxbetv164,
                MaxPerMatchV164 = setting.Maxpermatchv164,

                Credit = message.Credit,
                DisableCasino = setting.Disablecasino,
                DisableNumber = setting.Disablenumber,
                Recommend = message.ActorId,
                Ugroup = setting.Ugroup,
                Closed = setting.Closed,

                Apositiontaking_1 = setting.Apositiontaking_1,
                Apositiontaking_2 = setting.Apositiontaking_2,
                Apositiontaking_3 = setting.Apositiontaking_3,
                Apositiontaking_4 = setting.Apositiontaking_4,
                Apositiontaking_5 = setting.Apositiontaking_5,
                Apositiontaking_6 = setting.Apositiontaking_6,
                Apositiontaking_7 = setting.Apositiontaking_7,
                Apositiontaking_8 = setting.Apositiontaking_8,
                Apositiontaking_9 = setting.Apositiontaking_9,
                Apositiontaking_10 = setting.Apositiontaking_10,

                V2apositiontaking_10 = setting.V2apositiontaking_10,
                V3apositiontaking_10 = setting.V3apositiontaking_10,
                V5apositiontaking_10 = setting.V5apositiontaking_10,
                V8apositiontaking_10 = setting.V8apositiontaking_10,
                V10apositiontaking_10 = setting.V10apositiontaking_10,
                V11apositiontaking_10 = setting.V11apositiontaking_10,
                V99apositiontaking_10 = setting.V99apositiontaking_10,

                Apositiontaking_9999 = setting.Apositiontaking_9999,

                Alivepositiontaking_1 = setting.Alivepositiontaking_1,
                Alivepositiontaking_3 = setting.Alivepositiontaking_3,
                Alivepositiontaking_7 = setting.Alivepositiontaking_7,
                Alivepositiontaking_8 = setting.Alivepositiontaking_8,

                V2apositiontaking_1 = setting.V2apositiontaking_1,
                V2apositiontaking_2 = setting.V2apositiontaking_2,
                V2apositiontaking_3 = setting.V2apositiontaking_3,
                V2apositiontaking_7 = setting.V2apositiontaking_7,
                V2apositiontaking_8 = setting.V2apositiontaking_8,
                V2apositiontaking_20 = setting.V2apositiontaking_20,
                V2apositiontaking_9999 = setting.V2apositiontaking_9999,

                V3apositiontaking_1 = setting.V3apositiontaking_1,
                V3apositiontaking_2 = setting.V3apositiontaking_2,
                V3apositiontaking_3 = setting.V3apositiontaking_3,
                V3apositiontaking_20 = setting.V3apositiontaking_20,

                V5apositiontaking_1 = setting.V5apositiontaking_1,
                V5apositiontaking_2 = setting.V5apositiontaking_2,
                V5apositiontaking_3 = setting.V5apositiontaking_3,
                V5apositiontaking_20 = setting.V5apositiontaking_20,
                V5apositiontaking_9999 = setting.V5apositiontaking_9999,

                V8apositiontaking_1 = setting.V8apositiontaking_1,
                V8apositiontaking_3 = setting.V8apositiontaking_3,
                V8apositiontaking_20 = setting.V8apositiontaking_20,

                V10apositiontaking_1 = setting.V10apositiontaking_1,
                V10apositiontaking_2 = setting.V10apositiontaking_2,
                V10apositiontaking_3 = setting.V10apositiontaking_3,
                V10apositiontaking_20 = setting.V10apositiontaking_20,

                V11apositiontaking_1 = setting.V11apositiontaking_1,
                V11apositiontaking_20 = setting.V11apositiontaking_20,

                V99apositiontaking_1 = setting.V99apositiontaking_1,
                V99apositiontaking_2 = setting.V99apositiontaking_2,
                V99apositiontaking_3 = setting.V99apositiontaking_3,
                V99apositiontaking_9 = setting.V99apositiontaking_9,
                V99apositiontaking_20 = setting.V99apositiontaking_20,
                V99apositiontaking_9999 = setting.V99apositiontaking_9999,

                V161apositiontaking_81 = setting.V161apositiontaking_81,
                V161apositiontaking_83 = setting.V161apositiontaking_83,
                V161apositiontaking_86 = setting.V161apositiontaking_86,
                V161apositiontaking_88 = setting.V161apositiontaking_88,

                V161alivepositiontaking_85 = setting.V161alivepositiontaking_85,
                V161alivepositiontaking_86 = setting.V161alivepositiontaking_86,
                V161alivepositiontaking_87 = setting.V161alivepositiontaking_87,
                V161alivepositiontaking_89 = setting.V161alivepositiontaking_89,
                V161alivepositiontaking_90 = setting.V161alivepositiontaking_90,

                IP = message.IP,
                SystemId = 2,
                Language = message.Language,
                SubAccId = message.SubAccId,
                IsSearchAndCopy = true,

            };

            return qrCodeMemberSignInService.CreateMemberByQRCode(command);
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
            var link = "https://nexdev.net/issues/129373";
            return new ShowQRCodeViewModel(
                message.Credit, 
                message.CreditMin,
                message.CreditMax,
                message.SearchAccountName,
                message.SearchAccountId, 
                message.FirstName, 
                qrCodeAdapter.Generate(link), 
                link);
        }
    }
}
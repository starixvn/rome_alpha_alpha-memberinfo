namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Web.Models;
    using MediatR;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class QRCodeMemberSignInHandler
        : IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, Result>,
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
            return new QRCodeMemberSignInViewModel(message.MemberName, message.Credit.Equals(0) ? 1000 : message.Credit, message.ErrorMessage);
        }

        public Result Handle(CreateAccountByQRCodeRequest message)
        {
            var link = "https://nexdev.net/issues/129373";
            return new Result(0, JsonConvert.SerializeObject(new MemberSiteTokenLink(qrCodeAdapter.Generate(link), link)));
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
                message.Username, 
                message.FirstName, 
                qrCodeAdapter.Generate(link), 
                link);
        }
    }
}
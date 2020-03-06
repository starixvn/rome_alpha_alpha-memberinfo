namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
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
        IRequestHandler<GetMemberCreditRequest, decimal>
    {
        private readonly QrCodeAdapter qrCodeAdapter;

        public QRCodeMemberSignInHandler(QrCodeAdapter qrCodeAdapter)
        {
            this.qrCodeAdapter = qrCodeAdapter;
        }

        public QRCodeMemberSignInViewModel Handle(QRCodeMemberSignInRequest message)
        {
            return new QRCodeMemberSignInViewModel();
        }

        public Result Handle(CreateAccountByQRCodeRequest message)
        {
            var link = "https://nexdev.net/issues/129373";
            return new Result(0, JsonConvert.SerializeObject(new MemberSiteTokenLink(qrCodeAdapter.Generate(link), link)));
        }

        public IEnumerable<UsernameSuggestion> Handle(UserNameSuggestionQuery message)
        {
            var results = new List<UsernameSuggestion>();
            for(int i = 1; i < 100; i++)
            {
                results.Add(new UsernameSuggestion($"Username test {i}", string.Empty, string.Empty, string.Empty));
            }

            return results;
        }

        public decimal Handle(GetMemberCreditRequest message)
        {
            return 100;
        }
    }
}
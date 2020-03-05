namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Web.Models;
    using MediatR;
    using Newtonsoft.Json;

    public class QRCodeMemberSignInHandler
        : IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, Result>
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
            return new Result(0, JsonConvert.SerializeObject(new MemberSiteTokenLink(qrCodeAdapter.Generate(link), link)) );
        }
    }
}
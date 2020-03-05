namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.Web.Models;
    using MediatR;

    public class QRCodeMemberSignInHandler
        : IRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>,
        IRequestHandler<CreateAccountByQRCodeRequest, Result>
    {
        public QRCodeMemberSignInHandler()
        {
        }

        public QRCodeMemberSignInViewModel Handle(QRCodeMemberSignInRequest message)
        {
            return new QRCodeMemberSignInViewModel();
        }

        public Result Handle(CreateAccountByQRCodeRequest message)
        {
            return new Result();
        }
    }
}
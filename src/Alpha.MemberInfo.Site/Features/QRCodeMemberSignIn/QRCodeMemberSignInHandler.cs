namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using MediatR;
    using System.Threading.Tasks;

    public class QRCodeMemberSignInHandler
        : IAsyncRequestHandler<QRCodeMemberSignInRequest, QRCodeMemberSignInViewModel>
    {
        public QRCodeMemberSignInHandler()
        {
        }

        public async Task<QRCodeMemberSignInViewModel> Handle(QRCodeMemberSignInRequest message)
        {
            return new QRCodeMemberSignInViewModel();
        }
    }
}
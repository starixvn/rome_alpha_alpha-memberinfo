namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using MediatR;

    public class QRCodeMemberSignInRequest :
        QRCodeBaseQuery,
        IRequest<QRCodeMemberSignInViewModel>
    {
        public QRCodeMemberSignInRequest(IUser viewer) : base(viewer)
        {
        }

        public string MemberName { get; set; }

        public decimal Credit { get; set; }

        public string ErrorMessage { get; set; }
    }
}
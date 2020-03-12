namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class QRCodeMemberSignInRequest :
        MemberInfoBaseQuery,
        IRequest<QRCodeMemberSignInViewModel>
    {
        public QRCodeMemberSignInRequest(IUser viewer) : base(viewer)
        {
            ActorName = viewer.Name;
        }

        public string MemberName { get; set; }

        public string ActorName { get; }

        public decimal Credit { get; set; }

        public string ErrorMessage { get; set; }

        public override bool IsValid() => true;
    }
}
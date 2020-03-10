namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ShowQRCodeRequest :
        MemberInfoBaseQuery,
        IRequest<ShowQRCodeViewModel>
    {
        public ShowQRCodeRequest(IUser viewer) : base(viewer)
        {
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public decimal Credit { get; set; }

        public override bool IsValid() => true;
    }
}
namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.Web.Models;
    using MediatR;

    public class CreateAccountByQRCodeRequest :
        MemberInfoBaseQuery,
        IRequest<Result>
    {
        public CreateAccountByQRCodeRequest(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid() => true;
    }
}
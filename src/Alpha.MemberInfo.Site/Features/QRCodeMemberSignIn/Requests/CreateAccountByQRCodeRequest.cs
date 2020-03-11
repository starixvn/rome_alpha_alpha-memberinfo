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
            ActorId = viewer.Id;
            IP = viewer.IP;
            Language = viewer.Language;
            SubAccId = viewer.SubAccId;
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public decimal Credit { get; set; }

        public int ActorId { get; }

        public string IP { get; }

        public string Language { get; }

        public int SubAccId { get; }

        public override bool IsValid() => true;
    }
}
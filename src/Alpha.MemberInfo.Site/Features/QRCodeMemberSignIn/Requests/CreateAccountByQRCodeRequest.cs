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

            Username = $"{viewer.Name}{FirstSuffix}{SecondSuffix}{ThirdSuffix}";
        }

        public string UsernameCopyFrom { get; set; } 

        public string Username { get; private set; }

        public string FirstName { get; set; }

        public string FirstSuffix { get; set; }

        public string SecondSuffix { get; set; }

        public string ThirdSuffix { get; set; }

        public decimal Credit { get; set; }

        public int ActorId { get; }

        public string IP { get; }

        public string Language { get; }

        public int SubAccId { get; }

        public override bool IsValid() => true;
    }
}
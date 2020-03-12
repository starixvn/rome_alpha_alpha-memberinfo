namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class CreateAccountByQRCodeRequest :
        MemberInfoBaseQuery,
        IRequest<ResultCreateMemberQRCode>
    {
        public CreateAccountByQRCodeRequest(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            IP = viewer.IP;
            Language = viewer.Language;
            SubAccId = viewer.SubAccId;
            ActorName = viewer.Name;
        }

        private string ActorName { get; }

        public string UsernameCopyFrom { get; set; } 

        public string Username => $"{ActorName}{FirstSuffix}{SecondSuffix}{ThirdSuffix}";

        public string FirstName { get; set; }

        public string FirstSuffix { get; set; }

        public string SecondSuffix { get; set; }

        public string ThirdSuffix { get; set; }

        public decimal Credit { get; set; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }


        public int ActorId { get; }

        public string IP { get; }

        public string Language { get; }

        public int SubAccId { get; }

        public override bool IsValid() => true;
    }
}
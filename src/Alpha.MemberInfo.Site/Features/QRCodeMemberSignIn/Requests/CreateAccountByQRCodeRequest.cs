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
            IP = viewer.IP;
            SubAccId = viewer.SubAccId;
        }

        public string UsernameCopyFrom { get; set; }

        public string Username => $"{CustName}{FirstSuffix}{SecondSuffix}{ThirdSuffix}";

        public string FirstName { get; set; }

        public string FirstSuffix { get; set; }

        public string SecondSuffix { get; set; }

        public string ThirdSuffix { get; set; }

        public decimal Credit { get; set; }

        public decimal CreditMin { get; set; }

        public decimal CreditMax { get; set; }

        public string IP { get; }

        public int SubAccId { get; }

        public override bool IsValid() => 
            !string.IsNullOrEmpty(UsernameCopyFrom)
            && Credit <= CreditMax
            && Credit >= CreditMin;
    }
}
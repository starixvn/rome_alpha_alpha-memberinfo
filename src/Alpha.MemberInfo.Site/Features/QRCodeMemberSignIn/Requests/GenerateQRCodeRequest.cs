namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class GenerateQRCodeRequest : QRCodeBaseQuery, IRequest<string>
    {
        public GenerateQRCodeRequest(IUser viewer) : base(viewer)
        {
        }

        public string SearchAccountName { get; set; }

        public string Domain { get; set; }

        public override bool IsValid() => 
            base.IsValid() && 
            !string.IsNullOrEmpty(SearchAccountName) && 
            !string.IsNullOrEmpty(Domain);
    }
}
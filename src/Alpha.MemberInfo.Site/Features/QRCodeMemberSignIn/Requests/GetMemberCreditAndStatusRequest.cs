namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class GetMemberCreditAndStatusRequest : QRCodeBaseQuery, IAsyncRequest<CreditAndStatusModel>
    {
        public GetMemberCreditAndStatusRequest(IUser viewer) : base(viewer)
        {
        }

        public string SearchAccountName { get; set; }

        public override bool IsValid() => 
            base.IsValid() && 
            !string.IsNullOrEmpty(SearchAccountName);
    }
}
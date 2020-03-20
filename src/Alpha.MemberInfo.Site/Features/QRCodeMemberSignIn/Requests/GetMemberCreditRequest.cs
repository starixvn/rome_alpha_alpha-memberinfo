namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class GetMemberCreditRequest : MemberInfoBaseQuery, IRequest<CreditRangeModel>
    {
        private readonly IUser viewer;

        public GetMemberCreditRequest(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public int ActorId => viewer.Id;

        public override bool IsValid() => !string.IsNullOrEmpty(SearchAccountName);
    }
}
namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.MemberRequest;

    public class MemberRequestsQuery :
        MemberInfoBaseQuery,
        IRequest<MemberRequestViewModel>
    {
        public MemberRequestsQuery(IUser viewer) : base(viewer)
        {
        }

        public int UserSelectedProductId { get; set; }

        public bool DisabledUserSelectedProduct { get; set; }

        public override bool IsValid() => base.IsValid() && CustLevel.IsAgentLevel();
    }
}
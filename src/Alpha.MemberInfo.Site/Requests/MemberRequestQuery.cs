namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.MemberRequest;

    public class MemberRequestQuery :
        MemberInfoBaseQuery,
        IRequest<MemberRequestViewModel>
    {
        public MemberRequestQuery()
        {
        }

        public MemberRequestQuery(IUser viewer) : base(viewer)
        {
        }

        public int UserSelectedProductId { get; set; }
    }
}
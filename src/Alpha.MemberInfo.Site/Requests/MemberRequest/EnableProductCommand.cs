namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using System.Collections.Generic;
    using MediatR;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;

    public class EnableProductCommand :
        MemberInfoBaseQuery,
        IRequest<IEnumerable<Result>>
    {
        public EnableProductCommand()
        {
        }

        public EnableProductCommand(IUser viewer) : base(viewer)
        {
        }

        public IEnumerable<MemberRequestModel> Members { get; set; }

        public int UserSelectedProductId { get; set; }
    }
}
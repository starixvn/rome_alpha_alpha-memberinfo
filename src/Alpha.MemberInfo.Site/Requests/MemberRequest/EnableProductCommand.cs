namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using System.Collections.Generic;
    using MediatR;
    using MemberInfo.Models;

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

        public string MembersInfo { get; set; }

        public int UserSelectedProductId { get; set; }
    }
}
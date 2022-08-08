namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Models;
    using MediatR;
    using System.Collections.Generic;

    public class ReachHistoryListQuery : ReachHistoryQueryBase, IAsyncRequest<IEnumerable<ReachHistoryDTO>>
    {
        public ReachHistoryListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
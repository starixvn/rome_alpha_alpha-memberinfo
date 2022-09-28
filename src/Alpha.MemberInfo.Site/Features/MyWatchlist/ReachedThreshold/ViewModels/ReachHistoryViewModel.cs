namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using System.Collections.Generic;

    public class ReachHistoryViewModel
    {
        public ReachHistoryViewModel(ReachHistoryQuery message, IEnumerable<ReachHistoryModel> reachHistories)
        {
            ReachHistories = reachHistories;
            ReachHistoryQuery = message;
        }

        public IEnumerable<ReachHistoryModel> ReachHistories { get; }

        public ReachHistoryQuery ReachHistoryQuery { get; }
    }
}
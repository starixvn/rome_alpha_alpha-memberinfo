namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using System.Collections.Generic;

    public class ReachHistoryViewModel
    {
        public ReachHistoryViewModel(IEnumerable<ReachHistoryModel> reachHistories)
        {
            ReachHistories = reachHistories;
        }

        public IEnumerable<ReachHistoryModel> ReachHistories { get; }
    }
}
namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
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
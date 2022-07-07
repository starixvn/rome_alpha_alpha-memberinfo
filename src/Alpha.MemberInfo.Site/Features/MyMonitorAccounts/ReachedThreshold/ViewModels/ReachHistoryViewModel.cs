namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using System.Collections.Generic;

    public class ReachHistoryViewModel
    {
        public ReachHistoryViewModel(string username, IEnumerable<ReachHistoryModel> reachHistories)
        {
            Username = username;
            ReachHistories = reachHistories;
        }

        public string Username { get; }

        public IEnumerable<ReachHistoryModel> ReachHistories { get; }
    }
}
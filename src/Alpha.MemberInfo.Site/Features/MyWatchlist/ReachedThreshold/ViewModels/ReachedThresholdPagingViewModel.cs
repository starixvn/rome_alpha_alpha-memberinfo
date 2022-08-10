namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using System.Collections.Generic;

    public class ReachedThresholdPagingViewModel
    {
        public ReachedThresholdPagingViewModel(ReachlistAccountReport reachlistAccountReport, ReachlistAccountsQuery request)
        {
            ReachlistAccounts = reachlistAccountReport.ReachlistAccounts;
            TotalItems = reachlistAccountReport.TotalItems;
        }

        public IEnumerable<ReachlistAccountModel> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
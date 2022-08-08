namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReachedThresholdPagingViewModel
    {
        public ReachedThresholdPagingViewModel(ReachlistAccountReport reachlistAccountReport, ReachlistAccountsQuery request)
        {
            ReachlistAccounts = reachlistAccountReport.ReachlistAccounts
                                    .Select(account => new ReachlistAccountDTO(account, request.LimitedDateToShowInfo == default(DateTime)));
            TotalItems = reachlistAccountReport.TotalItems;
        }

        public IEnumerable<ReachlistAccountDTO> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
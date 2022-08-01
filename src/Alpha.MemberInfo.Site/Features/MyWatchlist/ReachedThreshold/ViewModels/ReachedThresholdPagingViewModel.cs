namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReachedThresholdPagingViewModel
    {
        public ReachedThresholdPagingViewModel(ReachlistAccountDTO reachlistAccountDTO, ReachlistAccountsQuery request)
        {
            ReachlistAccounts = reachlistAccountDTO.ReachlistAccounts
                                    .Select(account => new ReachlistAccountViewModel(account, request.LimitedDateToShowInfo == default(DateTime)));
            TotalItems = reachlistAccountDTO.TotalItems;
        }

        public IEnumerable<ReachlistAccountViewModel> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
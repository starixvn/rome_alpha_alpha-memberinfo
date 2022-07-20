namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests;
    using System.Collections.Generic;
    using System.Linq;

    public class ReachedThresholdPagingViewModel
    {
        public ReachedThresholdPagingViewModel(ReachlistAccountDTO reachlistAccountDTO, ReachlistAccountsQuery request)
        {
            ReachlistAccounts = reachlistAccountDTO.ReachlistAccounts
                                    .Select(account => new ReachlistAccountViewModel(account, request.LimitedDateToShowInfo));
            TotalItems = reachlistAccountDTO.TotalItems;
        }

        public IEnumerable<ReachlistAccountViewModel> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
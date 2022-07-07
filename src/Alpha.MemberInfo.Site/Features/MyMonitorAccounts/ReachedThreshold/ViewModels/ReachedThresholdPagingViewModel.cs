namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ReachedThresholdPagingViewModel
    {
        public ReachedThresholdPagingViewModel(ReachlistAccountDTO reachlistAccountDTO)
        {
            ReachlistAccounts = reachlistAccountDTO.ReachlistAccounts
                                    .Select(account => new ReachlistAccountViewModel(account));
            TotalItems = reachlistAccountDTO.TotalItems;
        }

        public IEnumerable<ReachlistAccountViewModel> ReachlistAccounts { get; }

        public int TotalItems { get; }
    }
}
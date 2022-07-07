namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MyWatchlistAccountsPagingViewModel
    {
        public MyWatchlistAccountsPagingViewModel(WatchlistAccountDTO watchlistAccountDTO)
        {
            WatchlistAccounts = watchlistAccountDTO.WatchlistAccounts
                                    .Select(account => new WatchlistAccountViewModel(account));
            TotalItems = watchlistAccountDTO.TotalItems;
        }

        public IEnumerable<WatchlistAccountViewModel> WatchlistAccounts { get; }

        public int TotalItems { get; }
    }
}
namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using System.Collections.Generic;
    using System.Linq;

    public class MyWatchlistAccountsPagingViewModel
    {
        public MyWatchlistAccountsPagingViewModel(WatchlistAccountDTO watchlistAccountDTO, WatchlistAccountsQuery request)
        {
            WatchlistAccounts = watchlistAccountDTO.WatchlistAccounts
                                    .Select(account => new WatchlistAccountViewModel(account, request.LimitedDateToShowInfo));
            TotalItems = watchlistAccountDTO.TotalItems;
        }

        public IEnumerable<WatchlistAccountViewModel> WatchlistAccounts { get; }

        public int TotalItems { get; }
    }
}
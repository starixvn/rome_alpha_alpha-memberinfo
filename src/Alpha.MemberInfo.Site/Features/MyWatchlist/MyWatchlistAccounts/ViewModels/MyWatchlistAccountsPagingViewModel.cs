namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using System.Collections.Generic;
    using System.Linq;

    public class MyWatchlistAccountsPagingViewModel
    {
        public MyWatchlistAccountsPagingViewModel(WatchlistAccountReport watchlistAccountReport, WatchlistAccountsQuery request)
        {
            WatchlistAccounts = watchlistAccountReport.WatchlistAccounts
                                    .Select(account => new WatchlistAccountDTO(account, request.LimitedDateToShowInfo));
            TotalItems = watchlistAccountReport.TotalItems;
        }

        public IEnumerable<WatchlistAccountDTO> WatchlistAccounts { get; }

        public int TotalItems { get; }
    }
}
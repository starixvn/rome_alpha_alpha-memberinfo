namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;

    public class MyWatchlistAccountsPagingViewModel
    {
        public MyWatchlistAccountsPagingViewModel(
            WatchlistAccountReport watchlistAccountReport,
            WatchlistSummaryModel watchlistSummary)
        {
            WatchlistAccounts = watchlistAccountReport.WatchlistAccounts;
            TotalItems = watchlistAccountReport.TotalItems;
            WatchlistSummary = watchlistSummary;
        }

        public IEnumerable<WatchlistAccountModel> WatchlistAccounts { get; }

        public int TotalItems { get; }

        public WatchlistSummaryModel WatchlistSummary { get; }
    }
}
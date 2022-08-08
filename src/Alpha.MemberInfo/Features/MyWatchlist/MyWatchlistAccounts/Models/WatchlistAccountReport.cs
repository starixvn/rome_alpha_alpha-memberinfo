namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using System.Collections.Generic;

    public class WatchlistAccountReport
    {
        public WatchlistAccountReport(IEnumerable<WatchlistAccountModel> watchlistAccounts, int totalItems)
        {
            WatchlistAccounts = watchlistAccounts;
            TotalItems = totalItems;
        }

        public IEnumerable<WatchlistAccountModel> WatchlistAccounts { get; }

        public int TotalItems { get; }
    }
}

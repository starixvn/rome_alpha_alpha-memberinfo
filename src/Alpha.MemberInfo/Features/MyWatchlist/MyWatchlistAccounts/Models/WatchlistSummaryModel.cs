namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    public class WatchlistSummaryModel
    {
        public int ActiveMembers { get; set; }

        public int MonitoredMembers { get; set; }

        public int ReachedDailyLimitMembers { get; set; }

        public int UnlimitedThresholdMembers { get; set; }

        public string UnlimitedThresholdUsers { get; set; }
    }
}

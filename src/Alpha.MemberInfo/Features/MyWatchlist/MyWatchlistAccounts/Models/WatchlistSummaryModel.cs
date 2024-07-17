namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using System;
    using Alpha.MemberInfo.Extensions;

    public class WatchlistSummaryModel
    {
        public int ActiveMembers { get; set; }

        public int MonitoredMembers { get; set; }

        public int ReachedDailyLimitMembers { get; set; }

        public int UnlimitedThresholdMembers { get; set; }

        public string UnlimitedThresholdUsers { get; set; }

        public string UnlimitedThresholdCustId { get; set; }

        public DateTime? MinAddedDate { get; set; }

        public string MinAddedDateString
            => MinAddedDate == null ? string.Empty : MinAddedDate.Value.ToShortDateWithSlash();
    }
}

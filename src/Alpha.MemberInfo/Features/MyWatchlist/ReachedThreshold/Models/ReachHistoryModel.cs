namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using System;

    public class ReachHistoryModel
    {
        public ReachHistoryModel()
        {
        }

        public DateTime CreatedDate { get; }

        public string LimitTypeReach { get; }

        public decimal WinLossAmount { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }
    }
}

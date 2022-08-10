namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    public class GlobalThresholdModel
    {
        public GlobalThresholdModel()
        {
        }

        public GlobalThresholdModel(decimal? winningThreshold, decimal? losingThreshold)
        {
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
        }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }
    }
}

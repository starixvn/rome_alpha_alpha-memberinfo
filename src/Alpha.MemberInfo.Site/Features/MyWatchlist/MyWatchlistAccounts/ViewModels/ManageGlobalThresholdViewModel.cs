namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;

    public class ManageGlobalThresholdViewModel
    {
        public ManageGlobalThresholdViewModel(GlobalThresholdModel globalThreshold)
        {
            Threshold = new ThresholdViewModel(globalThreshold?.WinningThreshold, globalThreshold?.LosingThreshold);
        }

        public ThresholdViewModel Threshold { get; }
    }
}
namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;

    public class ManageGlobalThresholdViewModel
    {
        public ManageGlobalThresholdViewModel(GlobalThresholdModel globalThreshold)
        {
            Threshold = new ThresholdViewModel(globalThreshold?.WinningThreshold, globalThreshold?.LosingThreshold);
        }

        public ThresholdViewModel Threshold { get; }
    }
}
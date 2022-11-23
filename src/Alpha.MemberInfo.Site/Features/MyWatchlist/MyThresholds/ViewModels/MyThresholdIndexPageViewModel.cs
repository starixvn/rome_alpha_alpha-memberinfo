namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.ViewModels
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests;
    using Alpha.MemberInfo.Site.ViewModels.Components;
    using Alpha.Web.Helpers;

    public class MyThresholdIndexPageViewModel
    {
        public MyThresholdIndexPageViewModel(
            MyThresholdIndexPageQuery message,
            bool isMobile,
            bool canUpdateAccount,
            GlobalThresholdModel myThreshold,
            IEnumerable<ReachHistoryModel> myThresholdHistory)
        {
            CustLevel = message.CustLevel;
            DateRangePickerViewModel = new DateRangePickerViewModel(
                message.MinDate,
                message.CurrentDate,
                message.FromDate,
                message.ToDate);
            IsMobile = isMobile;
            CanUpdateAccount = canUpdateAccount;
            MyThreshold = myThreshold;
            MyThresholdHistory = myThresholdHistory;
        }

        public UserLevel CustLevel { get; }

        public DateRangePickerViewModel DateRangePickerViewModel { get; }

        public bool IsMobile { get; }

        public bool CanUpdateAccount { get; }

        public GlobalThresholdModel MyThreshold { get; }

        public IEnumerable<ReachHistoryModel> MyThresholdHistory { get; }

        public string UserGuideLink
            => AppSettings.IsMBCSite
            ? MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=170467")
            : MicroSites.MessageUrl("~/StaticMessage/Guide?id=170467");
    }
}

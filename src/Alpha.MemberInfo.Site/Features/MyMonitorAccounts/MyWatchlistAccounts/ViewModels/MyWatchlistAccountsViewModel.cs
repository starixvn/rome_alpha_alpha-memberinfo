namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;

    public class MyWatchlistAccountsViewModel
    {
        public MyWatchlistAccountsViewModel(
            MyWatchlistAccountsQuery request,
            GlobalThresholdModel globalThresholdModel,
            bool isMobile,
            bool canUpdateAccount,
            MyWatchlistAccountsPagingViewModel myWatchListAccountPagingViewModel = null)
        {
            SearchAccountId = request.CustId;
            IsMobile = isMobile;
            CanUpdateAccount = canUpdateAccount;
            GlobalThresholdModel = globalThresholdModel;
            MyWatchListAccountPagingViewModel = myWatchListAccountPagingViewModel;
            LimitedDateToShowInfo = request.LimitedDateToShowInfo;
            CustLevel = request.CustLevel.DisplayName;
        }

        public int SearchAccountId { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public bool IsMobile { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }

        public bool LimitedDateToShowInfo { get; }

        public string CustLevel { get; }

        public bool CanUpdateAccount { get; }
    }
}
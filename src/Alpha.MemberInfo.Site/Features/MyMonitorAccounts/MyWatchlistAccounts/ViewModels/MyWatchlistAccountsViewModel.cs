namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;

    public class MyWatchlistAccountsViewModel
    {
        public MyWatchlistAccountsViewModel(
            MyWatchlistAccountsRequest request,
            GlobalThresholdModel globalThresholdModel,
            bool isMobile,
            MyWatchlistAccountsPagingViewModel myWatchListAccountPagingViewModel = null)
        {
            SearchAccountId = request.CustId;
            IsMobile = isMobile;
            GlobalThresholdModel = globalThresholdModel;
            MyWatchListAccountPagingViewModel = myWatchListAccountPagingViewModel;
        }

        public int SearchAccountId { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }

        public bool IsMobile { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }
    }
}
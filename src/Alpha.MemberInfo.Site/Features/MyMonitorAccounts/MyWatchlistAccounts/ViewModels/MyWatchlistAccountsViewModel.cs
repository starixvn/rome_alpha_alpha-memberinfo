namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;

    public class MyWatchlistAccountsViewModel
    {
        public MyWatchlistAccountsViewModel(
            MyWatchlistAccountsRequest request,
            GlobalThresholdModel globalThresholdModel,
            MyWatchlistAccountsPagingViewModel myWatchListAccountPagingViewModel = null)
        {
            SearchAccountId = request.CustId;

            GlobalThresholdModel = globalThresholdModel;
            MyWatchListAccountPagingViewModel = myWatchListAccountPagingViewModel;
        }

        public int SearchAccountId { get; }

        public MyWatchlistAccountsPagingViewModel MyWatchListAccountPagingViewModel { get; }

        public GlobalThresholdModel GlobalThresholdModel { get; }
    }
}
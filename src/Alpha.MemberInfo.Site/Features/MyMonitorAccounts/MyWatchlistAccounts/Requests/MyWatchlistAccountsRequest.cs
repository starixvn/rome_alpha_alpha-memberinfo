namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using MediatR;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Models;

    public class MyWatchlistAccountsRequest : MyWatchlistAccountsRequestBase, IAsyncRequest<MyWatchlistAccountsViewModel>
    {
        public MyWatchlistAccountsRequest(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
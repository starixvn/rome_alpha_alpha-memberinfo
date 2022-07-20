namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using MediatR;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Models;

    public class MyWatchlistAccountsQuery : MyWatchlistAccountsQueryBase, IAsyncRequest<MyWatchlistAccountsViewModel>
    {
        public MyWatchlistAccountsQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
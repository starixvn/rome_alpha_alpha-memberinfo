namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using MediatR;

    public class WatchlistAccountsQuery : MyWatchlistAccountsQueryBase, IAsyncRequest<MyWatchlistAccountsPagingViewModel>
    {
        public WatchlistAccountsQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
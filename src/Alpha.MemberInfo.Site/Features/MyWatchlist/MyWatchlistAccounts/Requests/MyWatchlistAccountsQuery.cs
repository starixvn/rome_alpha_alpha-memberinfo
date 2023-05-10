namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using MediatR;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Models;

    public class MyWatchlistAccountsQuery : MyWatchlistAccountsQueryBase, IAsyncRequest<MyWatchlistAccountsViewModel>
    {
        public MyWatchlistAccountsQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public bool IsOpenFromProblemAccount { get; set; }

        public string Ids { get; set; }

        public IUser Viewer { get; }
    }
}
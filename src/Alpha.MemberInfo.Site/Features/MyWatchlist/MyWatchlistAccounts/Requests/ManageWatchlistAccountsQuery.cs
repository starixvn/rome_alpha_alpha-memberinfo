namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ManageWatchlistAccountsQuery : MemberInfoBaseQuery, IAsyncRequest<ManageWatchlistAccountsViewModel>
    {
        public ManageWatchlistAccountsQuery(IUser viewer) : base(viewer)
        {
        }

        public string CustIds { get; set; }

        public string Usernames { get; set; }

        public decimal? WinningThreshold { get; set; }

        public decimal? LosingThreshold { get; set; }

        public bool IsEdit { get; set; }
    }
}
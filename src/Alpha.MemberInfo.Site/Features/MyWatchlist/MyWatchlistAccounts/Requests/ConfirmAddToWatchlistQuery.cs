namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ConfirmAddToWatchlistQuery : MemberInfoBaseQuery, IAsyncRequest<ConfirmAddToWatchlistViewModel>
    {
        public ConfirmAddToWatchlistQuery(IUser viewer) : base(viewer)
        {
        }

        public string Ids { get; set; }
    }
}
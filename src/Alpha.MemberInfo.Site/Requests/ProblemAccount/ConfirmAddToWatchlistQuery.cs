namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using MediatR;

    public class ConfirmAddToWatchlistQuery : MemberInfoBaseQuery, IAsyncRequest<ConfirmAddToWatchlistViewModel>
    {
        public ConfirmAddToWatchlistQuery(IUser viewer) : base(viewer)
        {
        }

        public string Ids { get; set; }
    }
}
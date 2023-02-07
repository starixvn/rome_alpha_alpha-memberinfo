namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class MyThresholdQuery : MemberInfoBaseQuery, IAsyncRequest<ThresholdViewModel>
    {
        public MyThresholdQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
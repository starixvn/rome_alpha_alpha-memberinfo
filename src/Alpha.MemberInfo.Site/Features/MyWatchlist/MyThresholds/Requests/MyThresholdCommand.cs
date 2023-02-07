namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using MediatR;

    public class MyThresholdCommand : UpdateGlobalThresholdCommand, IAsyncRequest<Result>
    {
        public MyThresholdCommand(IUser viewer) : base(viewer)
        {
        }
    }
}
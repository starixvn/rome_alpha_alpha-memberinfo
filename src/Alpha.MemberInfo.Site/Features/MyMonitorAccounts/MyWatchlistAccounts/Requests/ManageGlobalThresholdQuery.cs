namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ManageGlobalThresholdQuery : MemberInfoBaseQuery, IAsyncRequest<ManageGlobalThresholdViewModel>
    {
        public ManageGlobalThresholdQuery(IUser viewer) : base(viewer)
        {
        }

        public decimal? WinningThreshold { get; set; }

        public decimal? LosingThreshold { get; set; }
    }
}
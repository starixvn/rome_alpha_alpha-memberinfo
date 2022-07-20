namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using MediatR;

    public class ReachlistAccountsQuery : ReachedThresholdQueryBase, IAsyncRequest<ReachedThresholdPagingViewModel>
    {
        public ReachlistAccountsQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using MediatR;

    public class ReachedThresholdQuery : ReachedThresholdRequestBase, IAsyncRequest<ReachedThresholdPagingViewModel>
    {
        public ReachedThresholdQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
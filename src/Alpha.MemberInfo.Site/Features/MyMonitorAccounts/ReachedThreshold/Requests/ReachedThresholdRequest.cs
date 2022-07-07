namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using MediatR;

    public class ReachedThresholdRequest : ReachedThresholdRequestBase, IAsyncRequest<ReachedThresholdViewModel>
    {
        public ReachedThresholdRequest(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
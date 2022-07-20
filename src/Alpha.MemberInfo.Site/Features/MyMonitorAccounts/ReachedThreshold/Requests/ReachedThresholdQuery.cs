namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using MediatR;

    public class ReachedThresholdQuery : ReachedThresholdQueryBase, IAsyncRequest<ReachedThresholdViewModel>
    {
        public ReachedThresholdQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
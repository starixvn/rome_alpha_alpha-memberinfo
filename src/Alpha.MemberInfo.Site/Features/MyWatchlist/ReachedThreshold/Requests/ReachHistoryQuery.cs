namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels;
    using MediatR;

    public class ReachHistoryQuery : ReachHistoryQueryBase, IAsyncRequest<ReachHistoryViewModel>
    {
        public ReachHistoryQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
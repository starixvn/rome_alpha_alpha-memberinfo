namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class ManagePersonalThresholdPresetQuery : MemberInfoBaseQuery, IAsyncRequest<ManagePersonalThresholdPresetViewModel>
    {
        public ManagePersonalThresholdPresetQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
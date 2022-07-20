namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;

    public class ReachedThresholdExcelQuery : ReachedThresholdQueryBase, IAsyncRequest<ExcelFileViewModel>
    {
        public ReachedThresholdExcelQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
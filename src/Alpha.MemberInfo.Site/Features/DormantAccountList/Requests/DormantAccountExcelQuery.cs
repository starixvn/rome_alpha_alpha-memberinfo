namespace Alpha.MemberInfo.Site.Features.DormantAccountList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;

    public class DormantAccountExcelQuery : DormantAccountQuery, IAsyncRequest<ExcelFileViewModel>
    {
        public DormantAccountExcelQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
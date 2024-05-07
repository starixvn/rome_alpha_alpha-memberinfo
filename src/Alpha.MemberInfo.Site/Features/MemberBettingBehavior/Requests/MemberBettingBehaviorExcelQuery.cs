namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;

    public class MemberBettingBehaviorExcelQuery : MemberInfoBaseQuery, IAsyncRequest<ExcelFileViewModel>
    {
        public MemberBettingBehaviorExcelQuery(IUser viewer) : base(viewer)
        {

        }
    }
}
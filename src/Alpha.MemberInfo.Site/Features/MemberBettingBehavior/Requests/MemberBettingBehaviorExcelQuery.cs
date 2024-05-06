namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;

    public class MemberBettingBehaviorExcelQuery : IAsyncRequest<ExcelFileViewModel>
    {
        public MemberBettingBehaviorExcelQuery()
        {

        }
    }
}
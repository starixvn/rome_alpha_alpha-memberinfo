
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using System.Threading.Tasks;

    public class MemberBettingBehaviorHandler : ExcelBaseHandler, IAsyncRequestHandler<MemberBettingBehaviorExcelQuery, ExcelFileViewModel>
    {
        public MemberBettingBehaviorHandler()
        {

        }

        public Task<ExcelFileViewModel> Handle(MemberBettingBehaviorExcelQuery message)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetExcelTemplateDir()
        {
            throw new System.NotImplementedException();
        }
    }
}
namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Views;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using System.Threading.Tasks;

    public class ReachedThresholdExcelHandler : ExcelBaseHandler, IAsyncRequestHandler<ReachedThresholdExcelQuery, ExcelFileViewModel>
    {
        private readonly ReachedThresholdService service;
        private readonly ReachedThresholdWorkbook workbook;

        public ReachedThresholdExcelHandler(ReachedThresholdService service, ReachedThresholdWorkbook workbook)
        {
            this.service = service;
            this.workbook = workbook;
        }

        public async Task<ExcelFileViewModel> Handle(ReachedThresholdExcelQuery message)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetExcelTemplateDir()
        {
            throw new System.NotImplementedException();
        }
    }
}
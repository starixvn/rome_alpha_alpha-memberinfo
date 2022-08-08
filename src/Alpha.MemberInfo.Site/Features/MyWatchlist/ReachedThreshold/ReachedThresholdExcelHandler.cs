namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using static ONEbook.SpreadSheets.Column;

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
            var param = new SelectReachListParam(
                 message.CustId,
                 message.Username,
                 message.FromDate,
                 message.ToDate,
                 message.Status != 0 ? ReachedThresholdStatusConstants.GetReachedThresholdStatus(message.Status) : string.Empty,
                 message.LimitType,
                 message.SortType,
                 message.IsDesc,
                 int.MaxValue - 1,
                 1,
                 message.LimitedDateToShowInfo);

            var reachlistAccountReport = await service.GetReachlistAccounts(param);

            var isShowLastLoginColumn = message.LimitedDateToShowInfo == default(DateTime);
            var fileContent = workbook
               .SetData(reachlistAccountReport.ReachlistAccounts)
               .SetRowMappers(GetBodyRowMapper(message, isShowLastLoginColumn))
               .UseTemplate(GetTemplateInfo(isShowLastLoginColumn))
               .Sheet("Sheet 1")
               .Title(GetReportTitle(message))
               .Header(Resources.ResourceManager.GetString)
               .ExportToBytes();

            return new ExcelFileViewModel(GetExcelFileName(message), fileContent);
        }

        protected override string GetExcelTemplateDir() => "~/App_Data/ExcelTemplates/ReachedThreshold";

        internal Mapper<ReachlistAccountModel> GetBodyRowMapper(ReachedThresholdExcelQuery message, bool isShowLastLoginColumn)
        {
            var mapper = new Mapper<ReachlistAccountModel>()
                .Map(A, data => data.RowNumber)
                .Map(B, data => data.Username);

            if (isShowLastLoginColumn)
            {
                mapper
                    .Map(C, data => data.LastLogged.ToLongDateTime12hWithHyphen())
                    .Map(D, data => Resources.ResourceManager.GetString(data.Status))
                    .Map(E, data => data.LastReachedDate.ToLongDateTime12hWithHyphen())
                    .Map(F, data => Resources.ResourceManager.GetString(data.LastLimitTypeReach))
                    .Map(G, data => data.WinLossAmount.ToMoneyFormat())
                    .Map(H, data => data.WinningThreshold.ToMoneyFormatWithUnlimited())
                    .Map(I, data => data.LosingThreshold.ToMoneyFormatWithUnlimited())
                    .Map(J, data => data.ReachedCnt);
            }
            else
            {
                mapper
                    .Map(C, data => Resources.ResourceManager.GetString(data.Status))
                    .Map(D, data => data.LastReachedDate.ToLongDateTime12hWithHyphen())
                    .Map(E, data => Resources.ResourceManager.GetString(data.LastLimitTypeReach))
                    .Map(F, data => data.WinLossAmount.ToMoneyFormat())
                    .Map(G, data => data.WinningThreshold.ToMoneyFormatWithUnlimited())
                    .Map(H, data => data.LosingThreshold.ToMoneyFormatWithUnlimited())
                    .Map(I, data => data.ReachedCnt);
            }

            return mapper;
        }

        private static string GetExcelFileName(ReachedThresholdExcelQuery message)
            => $"{Resources.ReachedThreshold} {message.FromDate.ToShortDateWithHyphen()} -- {message.ToDate.ToShortDateWithHyphen()}";

        private TemplateInfo GetTemplateInfo(bool isShowLastLoginColumn)
            => new TemplateInfo(
                   GetExcelTemplatePath(isShowLastLoginColumn ? "ReachedThreshold" : "ReachedThresholdHideDateTime"),
                   noInfomationText: Resources.NoInformationIsAvailable,
                   titleStartIndex: 1,
                   headerStartIndex: 2,
                   columnStartName: A,
                   sheetIndex: 1,
                   bodyStartIndex: 3,
                   bodyEndIndex: 4);

        private static string GetReportTitle(ReachedThresholdExcelQuery message)
        {
            var title = new StringBuilder();

            title.Append($"{Resources.ReachedThreshold} - ");
            title.Append($"{Resources.From}: {message.FromDate.ToShortDateWithSlash()} - ");
            title.Append($"{Resources.To}: {message.ToDate.ToShortDateWithSlash()} - ");
            title.Append($"{Resources.Status}: " +
                $"{Resources.ResourceManager.GetString(ReachedThresholdStatusConstants.GetReachedThresholdStatus(message.Status))} - ");
            title.Append($"{Resources.ThresholdType}: " +
                $"{Resources.ResourceManager.GetString(ReachedThresholdLimitTypeConstants.GetReachedThresholdLimitType(message.LimitType))} - ");
            title.Append($"{Resources.Username}: {message.Username}");

            return title.ToString();
        }
    }
}
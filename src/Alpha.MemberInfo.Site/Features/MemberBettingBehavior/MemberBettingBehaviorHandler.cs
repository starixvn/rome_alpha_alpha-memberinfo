namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.MemberBettingBehavior;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using System.Threading.Tasks;
    using static ONEbook.SpreadSheets.Column;

    public class MemberBettingBehaviorHandler : ExcelBaseHandler,
        IAsyncRequestHandler<MemberProfileQuery, CustomerProfile>,
        IAsyncRequestHandler<MemberBettingStatisticQuery, MemberBettingStatistic>,
        IAsyncRequestHandler<MemberBettingBehaviorExcelQuery, ExcelFileViewModel>
    {
        private readonly MemberBettingBehaviorService bettingBeharviorService;
        private readonly MemberBettingBehaviorWorkbook workbook;

        public MemberBettingBehaviorHandler(
            MemberBettingBehaviorService bettingBeharviorService,
            MemberBettingBehaviorWorkbook workbook)
        {
            this.bettingBeharviorService = bettingBeharviorService;
            this.workbook = workbook;            
        }

        public async Task<ExcelFileViewModel> Handle(MemberBettingBehaviorExcelQuery message)
        {
            var memberBettingBehaviors = await bettingBeharviorService.GetMemberBettingStatisticExcelData(message.MemberCustId, message.StartDate, message.EndDate, message.Site, message.LanguageKey);

            var fileName = $"{Resources.MemberBettingBehavior}-{message.UserName}-{message.EndDate.AddDays(-message.DateRange).ToString("MM/dd/yyyy")}-{message.EndDate.ToString("MM/dd/yyyy")}";
            var fileContent = workbook
                .SetData(memberBettingBehaviors.BettingStatisticExportRows, memberBettingBehaviors.Total)
                .SetRowMappers(GetNormalRowMapper(), GetTotalRowMapper())
                .UseTemplate(GetTemplateInfo())
                .Title(fileName)
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel(fileName, fileContent);
        }

        private static Mapper<BettingStatisticExportRow> GetBodyRowMapper()
            => new Mapper<BettingStatisticExportRow>()
                .Map(C, bettingStatistics => bettingStatistics.WinningWinLoss)
                .Map(D, bettingStatistics => bettingStatistics.LosingWinLoss)
                .Map(E, bettingStatistics => bettingStatistics.TotalWinLoss)
                .Map(F, bettingStatistics => bettingStatistics.Margin)
                .Map(G, bettingStatistics => bettingStatistics.LiveTurnover)
                .Map(H, bettingStatistics => bettingStatistics.NonLiveTurnover)
                .Map(I, bettingStatistics => bettingStatistics.MixTurnover)
                .Map(J, bettingStatistics => bettingStatistics.Turnover);

        private static Mapper<BettingStatisticExportRow> GetNormalRowMapper()
            => GetBodyRowMapper()
                .Map(A, bettingStatistics => bettingStatistics.Sporttype)
                .Map(B, bettingStatistics => bettingStatistics.Bettype);

        private static Mapper<BettingStatisticExportRow> GetTotalRowMapper()
            => GetBodyRowMapper().Map(A, total => Resources.Total);

        private TemplateInfo GetTemplateInfo()
            => new TemplateInfo(GetExcelTemplatePath("MemberBettingBehavior"), Resources.NoInformationIsAvailable, bodyStartIndex: 4);

        protected override string GetExcelTemplateDir() => "~/App_Data/ExcelTemplates/MemberBettingBehavior";

        public async Task<CustomerProfile> Handle(MemberProfileQuery message)
        {
            var profile = await bettingBeharviorService.GetCustomerProfile(message.MemberCustId, message.Site);
            profile.CanUpdateAccount = message.CanUpdateAccount;

            if (message.ViewerLevelId != UserLevelConstant.Agent)
            {
                profile.IsMonitoredMember = null;
            }

            return profile;
        }

        public Task<MemberBettingStatistic> Handle(MemberBettingStatisticQuery message)
            => bettingBeharviorService.GetMemberBettingBehaviorStatistics(
                message.MemberCustId,
                message.StartDate,
                message.EndDate,
                message.Site,
                message.LanguageKey);
    }
}
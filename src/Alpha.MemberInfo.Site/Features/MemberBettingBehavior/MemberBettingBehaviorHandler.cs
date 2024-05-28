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
        private readonly MemberBettingBehaviorService service;
        private readonly MemberBettingBehaviorWorkbook workbook;

        public MemberBettingBehaviorHandler(MemberBettingBehaviorService service, MemberBettingBehaviorWorkbook workbook)
        {
            this.service = service;
            this.workbook = workbook;            
        }

        public async Task<ExcelFileViewModel> Handle(MemberBettingBehaviorExcelQuery message)
        {
            var memberBettingBehaviors = await service.GetMemberBettingStatisticExcelData(message.MemberCustId, message.StartDate, message.EndState, message.Site);

            var fileContent = workbook
                .SetData(memberBettingBehaviors.BettingStatisticExportRows, memberBettingBehaviors.Total)
                .SetRowMappers(GetNormalRowMapper(), GetTotalRowMapper())
                .UseTemplate(GetTemplateInfo())
                .Title(string.Empty)
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel("MemberBettingBehavior", fileContent);
        }

        private static Mapper<BettingStatisticExportRow> GetBodyRowMapper()
        {
            var mapper = new Mapper<BettingStatisticExportRow>()
                .Map(C, bettingStatistics => bettingStatistics.WinBetCount)
                .Map(D, bettingStatistics => bettingStatistics.LoseBetCount)
                .Map(E, bettingStatistics => bettingStatistics.DrawBetCount)
                .Map(F, bettingStatistics => bettingStatistics.CanceledBetCount)
                .Map(G, bettingStatistics => bettingStatistics.TotalBetCount)
                .Map(H, bettingStatistics => bettingStatistics.WinningWinLoss)
                .Map(I, bettingStatistics => bettingStatistics.LosingWinLoss)
                .Map(J, bettingStatistics => bettingStatistics.TotalWinLoss)
                .Map(K, bettingStatistics => bettingStatistics.Margin)
                .Map(L, bettingStatistics => bettingStatistics.AverageTurnover)
                .Map(M, bettingStatistics => bettingStatistics.LiveTurnover)
                .Map(N, bettingStatistics => bettingStatistics.NonLiveTurnover)
                .Map(O, bettingStatistics => bettingStatistics.MixTurnover)
                .Map(P, bettingStatistics => bettingStatistics.Turnover)
                .Map(Q, bettingStatistics => bettingStatistics.NetTurnOver);

            return mapper;
        }

        private static Mapper<BettingStatisticExportRow> GetNormalRowMapper()
        {
            var mapper = GetBodyRowMapper()
                .Map(A, bettingStatistics => bettingStatistics.Sporttype)
                .Map(B, bettingStatistics => bettingStatistics.Bettype);

            return mapper;
        }

        private static Mapper<BettingStatisticExportRow> GetTotalRowMapper()
            => GetBodyRowMapper().Map(A, total => Resources.Total);

        private TemplateInfo GetTemplateInfo()
            => new TemplateInfo(GetExcelTemplatePath("MemberBettingBehavior"), Resources.NoInformationIsAvailable, bodyStartIndex: 4);

        protected override string GetExcelTemplateDir()
            => "~/App_Data/ExcelTemplates/MemberBettingBehavior";

        public async Task<CustomerProfile> Handle(MemberProfileQuery message)
        {
            var profile = await service.GetCustomerProfile(message.MemberCustId, message.Site);
            if(message.ViewerLevelId != UserLevelConstant.Agent)
            {
                profile.IsMonitoredMember = null;
            }
            return profile;
        }

        public async Task<MemberBettingStatistic> Handle(MemberBettingStatisticQuery message)
            => await service.GetMemberBettingBehaviorStatistics(message.MemberCustId, message.StartDate, message.EndState, message.Site);
    }
}
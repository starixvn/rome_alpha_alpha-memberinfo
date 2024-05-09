
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.MemberBettingBehavior;
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using System.Threading.Tasks;
    using static ONEbook.SpreadSheets.Column;

    public class MemberBettingBehaviorHandler : ExcelBaseHandler, IAsyncRequestHandler<MemberBettingBehaviorExcelQuery, ExcelFileViewModel>
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
            var memberBettingBehaviors = await service.GetMemberBettinBehaviorStatistics();

            var fileContent = workbook
                .SetData(memberBettingBehaviors)
                .SetRowMappers(GetBodyRowMapper())
                .UseTemplate(GetTemplateInfo())
                .Title(string.Empty)
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel("MemberBettingBehavior", fileContent);
        }

        private static Mapper<MemberBettingStatistic> GetBodyRowMapper()
        {
            var mapper = new Mapper<MemberBettingStatistic>()
                .Map(A, memberBettingBehavior => memberBettingBehavior.Sports)
                .Map(B, memberBettingBehavior => memberBettingBehavior.BetCount.Win)
                .Map(C, memberBettingBehavior => memberBettingBehavior.BetCount.Lose)
                .Map(D, memberBettingBehavior => memberBettingBehavior.BetCount.Draw)
                .Map(E, memberBettingBehavior => memberBettingBehavior.BetCount.Canceled)
                .Map(F, membetBettingBehavior => membetBettingBehavior.BetCount.Total)
                .Map(G, membetBettingBehavior => membetBettingBehavior.WinLoss.Winning)
                .Map(H, membetBettingBehavior => membetBettingBehavior.WinLoss.Losing)
                .Map(I, membetBettingBehavior => membetBettingBehavior.WinLoss.Total)
                .Map(J, membetBettingBehavior => membetBettingBehavior.WinLoss.Margin)
                .Map(K, membetBettingBehavior => membetBettingBehavior.Stake.Average)
                .Map(L, membetBettingBehavior => membetBettingBehavior.Stake.TurnoverLive)
                .Map(M, membetBettingBehavior => membetBettingBehavior.Stake.TurnoverNonLive)
                .Map(N, membetBettingBehavior => membetBettingBehavior.Stake.TotalTurnover)
                .Map(O, membetBettingBehavior => membetBettingBehavior.Stake.NetTurnover);

            return mapper;
        }

        private TemplateInfo GetTemplateInfo()
            => new TemplateInfo(GetExcelTemplatePath("MemberBettingBehavior"), Resources.NoInformationIsAvailable, bodyStartIndex: 4);

        protected override string GetExcelTemplateDir()
            => "~/App_Data/ExcelTemplates/MemberBettingBehavior";
    }
}
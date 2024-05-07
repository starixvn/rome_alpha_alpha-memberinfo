
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using System.Threading.Tasks;
    using static ONEbook.SpreadSheets.Column;

    public class MemberBettingBehaviorHandler : ExcelBaseHandler, IAsyncRequestHandler<MemberBettingBehaviorExcelQuery, ExcelFileViewModel>
    {
        private readonly MemberBettingBehaviorWorkbook workbook;

        public MemberBettingBehaviorHandler(MemberBettingBehaviorWorkbook workbook)
        {
            this.workbook = workbook;            
        }

        public async Task<ExcelFileViewModel> Handle(MemberBettingBehaviorExcelQuery message)
        {
            var memberBettingBehaviors = new MemberBettingBehavior[]
            {
                new MemberBettingBehavior {Win = 1, Lose = 1, Canceled = 1, Total = 3},
                new MemberBettingBehavior {Win = 1, Lose = 1, Canceled = 1, Total = 3},
                new MemberBettingBehavior {Win = 1, Lose = 1, Canceled = 1, Total = 3},
            };

            var fileContent = workbook
                .SetData(memberBettingBehaviors)
                .SetRowMappers(GetBodyRowMapper())
                .UseTemplate(GetTemplateInfo())
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel("test", fileContent);
        }

        private static Mapper<MemberBettingBehavior> GetBodyRowMapper()
        {
            var mapper = new Mapper<MemberBettingBehavior>()
                .Map(B, memberBettingBehavior => memberBettingBehavior.Win)
                .Map(C, memberBettingBehavior => memberBettingBehavior.Lose)
                .Map(D, memberBettingBehavior => memberBettingBehavior.Canceled);

            return mapper;
        }

        private TemplateInfo GetTemplateInfo()
            => new TemplateInfo(GetExcelTemplatePath("MemberBettingBehavior"), Resources.NoInformationIsAvailable, bodyStartIndex: 3);

        protected override string GetExcelTemplateDir()
            => "~/App_Data/ExcelTemplates/MemberBettingBehavior";
    }
}
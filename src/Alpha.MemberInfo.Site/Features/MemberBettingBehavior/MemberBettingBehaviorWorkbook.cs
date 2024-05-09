
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using ONEbook.SpreadSheets;
    using ONEbook.SpreadSheets.NPOI;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface MemberBettingBehaviorWorkbook
    {
        Workbook<MemberBettingStatistic> UseTemplate(TemplateInfo templateInfo);

        MemberBettingBehaviorWorkbook SetData(IEnumerable<MemberBettingStatistic> memberBettingBehaviors);

        MemberBettingBehaviorWorkbook SetRowMappers(Mapper<MemberBettingStatistic> memberBettingBehaviorMapper);
    }

    public class MemberBettingBehaviorWorkbookImpl : NpoiWorkbook<MemberBettingStatistic>, MemberBettingBehaviorWorkbook
    {
        private const int DataSheetIndex = 1;
        private int currentRowIndex;

        internal IEnumerable<MemberBettingStatistic> MemberBettingBehaviors { get; set; }

        internal Mapper<MemberBettingStatistic> MemberBettingBehaviorMapper { get; set; }

        private Row NormalRowTemplate { get; set; }

        internal Row TotalRowTemplate { get; set; }

        public MemberBettingBehaviorWorkbook SetData(IEnumerable<MemberBettingStatistic> memberBettingBehaviors)
        {
            MemberBettingBehaviors = memberBettingBehaviors;
            return this;
        }

        public MemberBettingBehaviorWorkbook SetRowMappers(Mapper<MemberBettingStatistic> memberBettingBehaviorMapper)
        {
            MemberBettingBehaviorMapper = memberBettingBehaviorMapper;
            return this;
        }

        protected override Sheet LoadTemplateSheet()
        {
            var sheet = GetSheetAt(TemplateInfo.SheetIndex);
            var currentRowIndex = TemplateInfo.BodyStartIndex;

            NormalRowTemplate = sheet.GetRow(currentRowIndex++);

            var reportSheet = GetSheetAt(DataSheetIndex);
            reportSheet.FooterRowsTemplate = new List<Row> { TotalRowTemplate };
            // OddRowTemplate is for showing No Data.
            reportSheet.OddRowTemplate = NormalRowTemplate;
            
            return reportSheet;
        }

        protected override void FillBody(Sheet sheet)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException(nameof(sheet), "Sheet cannot be null");
            }

            RemoveTemplateRows(sheet);

            if (MemberBettingBehaviors.Any())
            {
                FillBodyRows(sheet);
                FillFooterRows(sheet);
            }
            else
            {
                FillNoInfoRow(sheet);
            }
        }

        protected override void RemoveTemplateRows(Sheet sheet)
            => Workbook.RemoveSheetAt(TemplateInfo.SheetIndex);

        protected override void FillBodyRows(Sheet sheet)
        {
            currentRowIndex = TemplateInfo.BodyStartIndex;

            foreach (var item in MemberBettingBehaviors)
            {
                var row = sheet.GetRow(currentRowIndex++);
                row.CopyCellsFrom(GetRowTemplate());
                FillCells(row, item, MemberBettingBehaviorMapper.Mappings);
            }
        }
        private Row GetRowTemplate()
        {
            Row templateRow;
            templateRow = NormalRowTemplate;

            return templateRow;
        }

    }
}
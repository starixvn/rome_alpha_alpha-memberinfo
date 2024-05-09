
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using ONEbook.SpreadSheets;
    using ONEbook.SpreadSheets.NPOI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class MemberBettingBehavior
    {
        public MemberBettingBehavior()
        {

        }

        public int Win { get; set; }

        public int Lose { get; set; }

        public int Canceled { get; set; }

        public int Total { get; set; }
    }

    public interface MemberBettingBehaviorWorkbook
    {
        Workbook<MemberBettingBehavior> UseTemplate(TemplateInfo templateInfo);

        MemberBettingBehaviorWorkbook SetData(IEnumerable<MemberBettingBehavior> memberBettingBehaviors);

        MemberBettingBehaviorWorkbook SetRowMappers(Mapper<MemberBettingBehavior> memberBettingBehaviorMapper);
    }

    public class MemberBettingBehaviorWorkbookImpl : NpoiWorkbook<MemberBettingBehavior>, MemberBettingBehaviorWorkbook
    {
        private const int DataSheetIndex = 1;
        private int currentRowIndex;

        internal IEnumerable<MemberBettingBehavior> MemberBettingBehaviors { get; set; }

        internal Mapper<MemberBettingBehavior> MemberBettingBehaviorMapper { get; set; }

        private Row NormalRowTemplate { get; set; }

        internal Row TotalRowTemplate { get; set; }

        public MemberBettingBehaviorWorkbook SetData(IEnumerable<MemberBettingBehavior> memberBettingBehaviors)
        {
            MemberBettingBehaviors = memberBettingBehaviors;
            return this;
        }

        public MemberBettingBehaviorWorkbook SetRowMappers(Mapper<MemberBettingBehavior> memberBettingBehaviorMapper)
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
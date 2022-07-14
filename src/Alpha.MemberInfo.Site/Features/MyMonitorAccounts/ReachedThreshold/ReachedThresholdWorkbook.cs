namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using ONEbook.SpreadSheets;
    using ONEbook.SpreadSheets.NPOI;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ReachedThresholdWorkbook
    {
        Workbook<ReachlistAccountModel> UseTemplate(TemplateInfo templateInfo);
        ReachedThresholdWorkbook SetData(IEnumerable<ReachlistAccountModel> data);
        ReachedThresholdWorkbook SetRowMappers(Mapper<ReachlistAccountModel> rowMapper);
    }

    public class ReachedThresholdWorkbookImpl : NpoiWorkbook<ReachlistAccountModel>, ReachedThresholdWorkbook
    {
        private const int ReportSheetIndex = 1;

        internal IEnumerable<ReachlistAccountModel> Data { get; set; }

        internal Mapper<ReachlistAccountModel> RowMapper { get; set; }

        internal Row RowTemplate { get; set; }

        public ReachedThresholdWorkbook SetData(IEnumerable<ReachlistAccountModel> data)
        {
            Data = data;

            return this;
        }

        public ReachedThresholdWorkbook SetRowMappers(Mapper<ReachlistAccountModel> rowMapper)
        {
            RowMapper = rowMapper;

            return this;
        }

        protected override Sheet LoadTemplateSheet()
        {
            var sheet = GetSheetAt(TemplateInfo.SheetIndex);

            RowTemplate = sheet.GetRow(TemplateInfo.BodyStartIndex);

            return GetSheetAt(ReportSheetIndex);
        }

        protected override void FillBody(Sheet sheet)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException(nameof(sheet), "Sheet cannot be null");
            }

            RemoveTemplateRows(sheet);

            var hasData = (Data != null && Data.Any());

            if (hasData)
            {
                FillBodyRows(sheet);
            }
            else
            {
                FillNoInfoRow(sheet);
            }
        }

        protected override void FillBodyRows(Sheet sheet)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException(nameof(sheet), "Sheet cannot be null");
            }

            var currentRowIndex = TemplateInfo.BodyStartIndex;

            foreach (var item in Data)
            {
                FillReachlistAccounts(sheet, currentRowIndex++, item);
            }
        }

        internal void FillReachlistAccounts(Sheet sheet, int rowIndex, ReachlistAccountModel account)
        {
            var row = sheet.GetRow(rowIndex);
            row.CopyCellsFrom(RowTemplate);
            FillCells(row, account, RowMapper.Mappings);
        }

        protected override void FillNoInfoRow(Sheet sheet)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException(nameof(sheet), "Sheet cannot be null");
            }

            var rowIndex = TemplateInfo.BodyStartIndex;
            var colIndex = Column.GetColIndex(TemplateInfo.ColumnStartName);

            sheet.SetCellRangeValue(
                rowIndex,
                colIndex,
                rowIndex,
                RowTemplate.LastCellIndex - 1,
                TemplateInfo.NoInfomationText);

            sheet.GetCell(rowIndex, colIndex).SetCenterHorizontalAlignment();
        }

        protected override void RemoveTemplateRows(Sheet sheet)
        {
            Workbook.RemoveSheetAt(TemplateInfo.SheetIndex);
        }

        protected override void FillSheetName(int index, string name)
        {
            Workbook.SetSheetName(ReportSheetIndex, name);
        }
    }
}
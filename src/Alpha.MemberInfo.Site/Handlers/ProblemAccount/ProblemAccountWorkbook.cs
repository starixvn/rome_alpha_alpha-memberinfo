using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alpha.MemberInfo.Constants;
using Alpha.MemberInfo.Models.ProblemAccount;
using ONEbook.SpreadSheets;
using ONEbook.SpreadSheets.NPOI;

namespace Alpha.MemberInfo.Site.Handlers
{
    public interface ProblemAccountWorkbook
    {
        Workbook<ProblemAccountModel> UseTemplate(TemplateInfo templateInfo);

        Workbook<ProblemAccountModel> Sheet(string sheetName);

        Workbook<ProblemAccountModel> Title(string titleText);

        Workbook<ProblemAccountModel> Header(Func<string, string> headerMapping);

        byte[] ExportToBytes();

        ProblemAccountWorkbook SetData(
            IEnumerable<ProblemAccountModel> data);

        ProblemAccountWorkbook SetRowMappers(
            Mapper<ProblemAccountModel> masterRowMapper);
    }

    public class ProblemAccountWorkbookImpl :
        NpoiWorkbook<ProblemAccountModel>,
        ProblemAccountWorkbook
    {
        private const int ReportSheetIndex = 1;

        internal IEnumerable<ProblemAccountModel> Data { get; set; }

        internal Mapper<ProblemAccountModel> RowMapper { get; set; }

        internal Row MatchTitleRowTemplate { get; set; }

        internal Row OpenRowTemplate { get; set; }

        internal Row CloseRowTemplate { get; set; }

        internal Row SuspendedRowTemplate { get; set; }

        internal Row DisabledRowTemplate { get; set; }

        public ProblemAccountWorkbook SetData(
            IEnumerable<ProblemAccountModel> data)
        {
            Data = data;

            return this;
        }

        public ProblemAccountWorkbook SetRowMappers(
            Mapper<ProblemAccountModel> rowMapper)
        {
            RowMapper = rowMapper;

            return this;
        }

        internal void FillTitle(Sheet sheet, int rowIndex, ProblemAccountModel data)
        {
            var row = sheet.GetRow(rowIndex);
            row.CopyCellsFrom(MatchTitleRowTemplate);

            var title = "Title";
            sheet.SetCellRangeValue(rowIndex, row.FirstCellIndex, rowIndex, row.LastCellIndex - 1, title);
        }
               

        internal void FillProblemAccount(
            Sheet sheet,
            int rowIndex,
            ProblemAccountModel account)
        {
            var row = sheet.GetRow(rowIndex);
            Row templateRow;
            switch(account.Status)
            {
                case ProblemAccountStatusConstants.Open:
                    templateRow = OpenRowTemplate;
                    break;
                case ProblemAccountStatusConstants.Suspended:
                    templateRow = SuspendedRowTemplate;
                    break;
                case ProblemAccountStatusConstants.Closed:
                    templateRow = CloseRowTemplate;
                    break;
                default:
                    templateRow = DisabledRowTemplate;
                    break;
            }

            row.CopyCellsFrom(templateRow);
            FillCells(row, account, RowMapper.Mappings);
        }

        

        protected override Sheet LoadTemplateSheet()
        {
            var sheet = GetSheetAt(TemplateInfo.SheetIndex);

            var currentRowIndex = TemplateInfo.BodyStartIndex;

            //MatchTitleRowTemplate = sheet.GetRow(currentRowIndex++);
            OpenRowTemplate = sheet.GetRow(currentRowIndex++);
            SuspendedRowTemplate = sheet.GetRow(currentRowIndex++);
            CloseRowTemplate = sheet.GetRow(currentRowIndex++);            
            DisabledRowTemplate = sheet.GetRow(currentRowIndex);

            return GetSheetAt(ReportSheetIndex);
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
                MatchTitleRowTemplate.LastCellIndex - 1,
                TemplateInfo.NoInfomationText);

            sheet.GetCell(rowIndex, colIndex).SetCenterHorizontalAlignment();
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
                FillProblemAccount(sheet, currentRowIndex++, item);
            }
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
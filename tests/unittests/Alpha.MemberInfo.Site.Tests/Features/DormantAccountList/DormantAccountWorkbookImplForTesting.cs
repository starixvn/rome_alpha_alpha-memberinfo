namespace Alpha.MemberInfo.Site.Tests.Features.DormantAccountList
{
    using Alpha.MemberInfo.Site.Features.DormantAccountList;
    using ONEbook.SpreadSheets;

    public class DormantAccountWorkbookImplForTesting : DormantAccountWorkbookImpl
    {
        public void CallFillBody(Sheet sheet)
        {
            FillBody(sheet);
        }

        public void CallFillBodyRows(Sheet sheet)
        {
            FillBodyRows(sheet);
        }

        public void CallFillFooterRows(Sheet sheet)
        {
            FillFooterRows(sheet);
        }

        public void CallFillNoInfoRow(Sheet sheet)
        {
            TemplateInfo = new TemplateInfo(string.Empty);

            FillNoInfoRow(sheet);
        }
    }
}

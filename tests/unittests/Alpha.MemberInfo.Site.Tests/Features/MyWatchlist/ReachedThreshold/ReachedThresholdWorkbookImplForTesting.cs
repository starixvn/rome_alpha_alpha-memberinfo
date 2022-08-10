namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold;
    using ONEbook.SpreadSheets;

    public class ReachedThresholdWorkbookImplForTesting : ReachedThresholdWorkbookImpl
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

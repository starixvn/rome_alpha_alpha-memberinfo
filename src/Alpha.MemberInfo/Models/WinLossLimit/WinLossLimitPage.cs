namespace Alpha.MemberInfo.Models.WinLossLimit
{
    using System.Collections.Generic;
    using System.Linq;

    public class WinLossLimitPage
    {
        public WinLossLimitPage()
        {
            NumberOfRows = 0;
            WinLossLimitDetail = Enumerable.Empty<WinLossLimitDetail>();
        }

        public WinLossLimitPage(int numberOfRows, IEnumerable<WinLossLimitDetail> winLossLimitDetail)
        {
            NumberOfRows = numberOfRows;
            WinLossLimitDetail = winLossLimitDetail;
        }

        public int NumberOfRows { get; }

        public IEnumerable<WinLossLimitDetail> WinLossLimitDetail { get; }
    }
}

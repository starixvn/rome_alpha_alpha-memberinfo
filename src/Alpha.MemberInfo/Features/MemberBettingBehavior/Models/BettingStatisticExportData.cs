namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System.Collections.Generic;

    public class BettingStatisticExportData
    {
        public BettingStatisticExportData() { }

        public IEnumerable<BettingStatisticExportRow> BettingStatisticExportRows { get; set; }

        public BettingStatisticExportRow Total { get; set; }
    }
}

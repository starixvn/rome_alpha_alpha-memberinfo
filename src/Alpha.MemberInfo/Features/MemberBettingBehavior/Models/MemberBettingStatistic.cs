namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System.Collections.Generic;

    public class MemberBettingStatistic
    {
        public MemberBettingStatistic(
             SummaryStatistic summaryStatistic,
             IEnumerable<TurnOverBySport> turnOverBySports,
             IEnumerable<TurnoverByBettype> turnoverByBettypes,
             IEnumerable<TurnoverWinLossByDate> turnoverWinLossByDates
            )
        {
            SummaryStatistic = summaryStatistic;
            TurnOverBySports = turnOverBySports;
            TurnoverByBettypes = turnoverByBettypes;
            TurnoverWinLossByDates = turnoverWinLossByDates;
        }

        public SummaryStatistic SummaryStatistic { get; set; }

        public IEnumerable<TurnOverBySport> TurnOverBySports { get; set; }

        public IEnumerable<TurnoverByBettype> TurnoverByBettypes { get; set; }

        public IEnumerable<TurnoverWinLossByDate> TurnoverWinLossByDates { get; set; }
    }
}

namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System.Collections.Generic;

    public class MemberBettingStatistic
    {
        public MemberBettingStatistic() {}

        public SummaryStatistic SummaryStatistic { get; set; }

        public IEnumerable<TurnOverBySport> TurnOverBySports { get; set; }

        public IEnumerable<TurnoverByBettype> TurnoverByBettypes { get; set; }

        public IEnumerable<TurnoverWinLossByDate> TurnoverWinLossByDates { get; set; }
    }
}

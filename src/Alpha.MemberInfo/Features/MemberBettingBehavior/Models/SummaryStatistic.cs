
namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SummaryStatistic
    {
        public SummaryStatistic()
        {

        }

        public decimal Turnover { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Margin { get; set; }

        public int BetCount { get; set; }
    }
}


namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MemberBettingStatistic
    {
        public MemberBettingStatistic()
        {
        }

        public string Sports { get; set; }

        public string BetType { get; set; }

        public BetCount BetCount { get; set; }

        public WinLoss WinLoss { get; set; }

        public Stake Stake { get; set; }
    }
}


namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Stake
    {
        public Stake()
        {

        }

        public decimal Average { get; set; }

        public decimal TurnoverLive { get; set; }

        public decimal TurnoverNonLive { get; set; }

        public decimal TotalTurnover { get; set; }

        public decimal NetTurnover { get; set; }
    }
}

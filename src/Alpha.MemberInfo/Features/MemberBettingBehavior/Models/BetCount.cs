
namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BetCount
    {
        public BetCount()
        {
            
        }

        public int Win { get; set; }

        public int Lose { get; set; }

        public int Draw { get; set; }

        public int Canceled { get; set; }

        public int Total { get; set; }
    }
}

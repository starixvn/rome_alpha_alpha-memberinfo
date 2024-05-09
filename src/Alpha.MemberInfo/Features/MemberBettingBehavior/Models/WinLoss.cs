
namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WinLoss
    {
        public WinLoss()
        {

        }

        public decimal Winning { get; set; }

        public decimal Losing { get; set; }

        public decimal Total { get; set; }

        public decimal Margin { get; set; }
    }
}

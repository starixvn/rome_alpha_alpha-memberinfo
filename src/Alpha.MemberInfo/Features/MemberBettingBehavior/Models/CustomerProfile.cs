
namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CustomerProfile
    {
        public CustomerProfile()
        {

        }

        public int CategoryID { get; set; }

        public bool IsMonitoredMember { get; set; }

        public bool IsProblemAccount { get; set; }
    }
}

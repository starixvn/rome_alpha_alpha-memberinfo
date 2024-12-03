using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class CustomerWinLossAmountQuery : MemberInfoBaseQuery
    {
        public CustomerWinLossAmountQuery(IUser viewer)
           : base(viewer)
        {
        }

        public int SportType { get; set; }

        public int BetType { get; set; }
    }
}
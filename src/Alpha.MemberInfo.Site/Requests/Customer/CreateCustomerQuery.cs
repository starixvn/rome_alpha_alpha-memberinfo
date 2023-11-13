using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class CreateCustomerQuery : MemberInfoBaseQuery
    {
        public CreateCustomerQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}
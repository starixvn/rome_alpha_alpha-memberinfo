using Alpha.MemberInfo.Models;
using Fanex.Customer.Client.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class CreateCustomerRequest : MemberInfoBaseQuery
    {
        public CreateCustomerRequest(IUser viewer)
            : base(viewer)
        {
        }

        public GeneralInfo GeneralInfo { get; set; }

        public string ProductGroups { get; set; }
    }
}
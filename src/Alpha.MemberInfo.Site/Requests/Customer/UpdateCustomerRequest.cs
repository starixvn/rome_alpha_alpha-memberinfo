using Alpha.MemberInfo.Models;
using Fanex.Customer.Client.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class UpdateCustomerRequest : MemberInfoBaseQuery
    {
        public UpdateCustomerRequest(IUser viewer)
            : base(viewer)
        {
        }

        public GeneralInfo GeneralInfo { get; set; }

        public string ProductGroups { get; set; }

        public byte RoleId { get; set; }

        public byte UplineRoleId { get; set; }
    }
}
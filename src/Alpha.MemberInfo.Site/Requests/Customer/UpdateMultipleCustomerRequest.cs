using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class UpdateMultipleCustomerRequest : MemberInfoBaseQuery
    {
        public UpdateMultipleCustomerRequest(IUser viewer)
           : base(viewer)
        {
        }

        public string ProductGroups { get; set; }

        public string CustomerIds { get; set; }

        public string CustomerNames { get; set; }

        public byte RoleId { get; set; }

        public byte UplineRoleId { get; set; }
    }
}
using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class EditCustomerQuery : MemberInfoBaseQuery
    {
        public EditCustomerQuery(IUser viewer)
           : base(viewer)
        {
        }

        public byte RoleId { get; set; }

        public int ProductGroupId { get; set; }
    }
}
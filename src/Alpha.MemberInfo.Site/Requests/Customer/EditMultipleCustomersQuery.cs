using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class EditMultipleCustomersQuery : MemberInfoBaseQuery
    {
        public EditMultipleCustomersQuery(IUser viewer)
           : base(viewer)
        {
        }

        public string CustIds { get; set; }

        public string Usernames { get; set; }

        public byte RoleId { get; set; }
    }
}
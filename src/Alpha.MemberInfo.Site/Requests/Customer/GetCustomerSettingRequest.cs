using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class GetCustomerSettingRequest : MemberInfoBaseQuery
    {
        public GetCustomerSettingRequest(IUser viewer) : base(viewer)
        {
        }

        public int ProductGroupId { get; set; } = 1;

        public bool IsGetUpLine { get; set; } = false;

        public byte RoleId { get; set; }

        public byte UplineRoleId { get; set; }
    }
}
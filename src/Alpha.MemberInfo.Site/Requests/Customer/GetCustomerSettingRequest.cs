using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Requests.Customer
{
    public class GetCustomerSettingRequest : MemberInfoBaseQuery
    {
        public GetCustomerSettingRequest(IUser viewer) : base(viewer)
        {
        }

        public int ProductGroupId { get; set; } = 0;

        public bool IsGetUpLine { get; set; } = false;

        public byte RoleId { get; set; }

        public byte UplineRoleId { get; set; }

        public string SiteLabel { get; set; }

        public int AgentId { get; set; } = 0;
    }
}
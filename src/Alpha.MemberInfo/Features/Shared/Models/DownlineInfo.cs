namespace Alpha.MemberInfo.Features.Shared.Models
{
    public class DownlineInfo
    {
        public DownlineInfo(bool isActive, int custId, bool isSubAcc, int roleId)
        {
            IsActive = isActive;
            CustId = custId;
            IsSubAcc = isSubAcc;
            RoleId = roleId;
        }

        public bool IsActive { get; }

        public int CustId { get; }

        public bool IsSubAcc { get; }

        public int RoleId { get; }
    }
}

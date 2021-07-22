namespace Alpha.MemberInfo.Models.IndirectLoginAccount
{
    using System;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Enumerations;

    public class IndirectLoginAccountItem
    {
        public IndirectLoginAccountItem()
        {
        }

        public IndirectLoginAccountItem(
            int custId,
            int subAccId,
            string username,
            byte level,
            bool enabled,
            bool closed,
            bool suspended,
            DateTime? lastVerifyPwd,
            DateTime? lastChangedPwdDate,
            byte locked)
        {
            CustId = custId;
            SubAccId = subAccId;
            Username = username;
            Level = level;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            LastVerifyPwd = lastVerifyPwd;
            LastChangedPwdDate = lastChangedPwdDate;
            Locked = locked;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public string Username { get; }

        public byte Level { get; }

        public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(Level);

        public string LevelName
        {
            get
            {
                if (CustLevel.IsSuperLevel())
                {
                    return Resources.Super;
                }

                if (CustLevel.IsMasterLevel())
                {
                    return Resources.Master;
                }

                return CustLevel.IsAgentLevel()
                    ? Resources.Agent
                    : Resources.Member;
            }
        }

        public bool Enabled { get; }

        public bool Closed { get; }

        public bool Suspended { get; }

        public string Status
        {
            get
            {
                if (!Enabled)
                {
                    return Resources.Disabled;
                }

                if (Closed)
                {
                    return Resources.Closed;
                }

                return Suspended
                    ? Resources.Suspended
                    : Resources.Open;
            }
        }

        public DateTime? LastVerifyPwd { get; }

        public DateTime? LastChangedPwdDate { get; }

        public byte Locked { get; }

        public bool IsLock => Locked != 0;

        public string LockedStatus => IsLock ? Resources.Yes : Resources.No2;
    }
}

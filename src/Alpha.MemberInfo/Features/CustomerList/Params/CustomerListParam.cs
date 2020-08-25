using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    public class CustomerListParam
    {
        public CustomerListParam(
            UserLevel custLevel,
            int userId,
            int directUplineId,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex,
            bool limitedDateToShowInfo)
        {
            UserId = userId;
            DirectUplineId = directUplineId;
            CustLevel = custLevel;
            Username = username;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            DoubleCommLosing = doubleCommLosing;
            ProductUnSync = productUnSync;
            PageSize = pageSize;
            PageIndex = pageIndex;
            LimitedDateToShowInfo = limitedDateToShowInfo;
        }

        public int UserId { get; }

        public int DirectUplineId { get; }

        public UserLevel CustLevel { get; }

        public string Username { get; }

        public bool? Enabled { get; }

        public bool? Closed { get; }

        public bool? Suspended { get; }

        public byte DoubleCommLosing { get; }

        public byte ProductUnSync { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public bool LimitedDateToShowInfo { get; }
    }
}
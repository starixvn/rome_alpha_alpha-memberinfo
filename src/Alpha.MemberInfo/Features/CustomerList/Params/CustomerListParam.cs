namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    using Alpha.MemberInfo.Models;

    public class CustomerListParam
    {
        public CustomerListParam(
            int userId,
            UserLevel directUplineCustLevel,
            int directUplineId,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            int pageSize,
            int pageIndex,
            bool limitedDateToShowInfo,
            bool showNickname,
            bool isShowMyanmarOdds)
        {
            UserId = userId;
            DirectUplineId = directUplineId;
            DirectUplineCustLevel = directUplineCustLevel;
            Username = username;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            DoubleCommLosing = doubleCommLosing;
            PageSize = pageSize;
            PageIndex = pageIndex;
            LimitedDateToShowInfo = limitedDateToShowInfo;
            ShowNickname = showNickname;
            IsShowMyanmarOdds = isShowMyanmarOdds;
        }

        public int UserId { get; }

        public int DirectUplineId { get; }

        public UserLevel DirectUplineCustLevel { get; }

        public string Username { get; }

        public bool? Enabled { get; }

        public bool? Closed { get; }

        public bool? Suspended { get; }

        public byte DoubleCommLosing { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public bool LimitedDateToShowInfo { get; }

        public bool ShowNickname { get; }

        public bool IsShowMyanmarOdds { get; }
    }
}
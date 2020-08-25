namespace Alpha.MemberInfo.Site.Features.CustomerList.Params
{
    public class CustomerListParamBase
    {
        public CustomerListParamBase(
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
namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Fanex.Data.Repository;

    public class SelectCustomerListCriteriaBase : CriteriaBase
    {
        public SelectCustomerListCriteriaBase(
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex)
        {
            Username = username;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            DoubleCommLosing = doubleCommLosing;
            ProductUnSync = productUnSync;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string Username { get; }

        public bool? Enabled { get; }

        public bool? Closed { get; }

        public bool? Suspended { get; }

        public byte DoubleCommLosing { get; }

        public byte ProductUnSync { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => true;
    }
}

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Fanex.Data.Repository;

    public class SelectMasterListCriteria : CriteriaBase
    {
        public SelectMasterListCriteria(
            int srecommend,
            string username,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte doubleCommLosing,
            byte productUnSync,
            int pageSize,
            int pageIndex)
        {
            SRecommend = srecommend;
            Username = username;
            Enabled = enabled;
            Closed = closed;
            Suspended = suspended;
            DoubleCommLosing = doubleCommLosing;
            ProductUnSync = productUnSync;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }


        public int SRecommend { get; }

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

        public override string GetSettingKey() => "Acc_CustomerSelBySAg";

        public override bool IsValid() => SRecommend > 0;
    }
}

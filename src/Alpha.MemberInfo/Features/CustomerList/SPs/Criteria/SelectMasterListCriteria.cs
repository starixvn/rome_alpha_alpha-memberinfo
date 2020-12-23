namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class SelectMasterListCriteria : SelectCustomerListCriteriaBase
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
            int pageIndex) : base(username,
            enabled,
            closed,
            suspended,
            doubleCommLosing,
            productUnSync,
            pageSize,
            pageIndex)
        {
            SRecommend = srecommend;
        }


        public int SRecommend { get; }

        public override string GetSettingKey() => "Age_CustomerSelBySAg";

        public override bool IsValid() => SRecommend > 0;
    }
}
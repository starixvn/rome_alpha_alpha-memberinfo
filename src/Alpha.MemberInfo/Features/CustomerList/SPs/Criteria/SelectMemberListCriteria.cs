namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class SelectMemberListCriteria : SelectCustomerListCriteriaBase
    {
        public SelectMemberListCriteria(
            int userId,
            int recommend,
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
            Recommend = recommend;
            UserId = userId;
        }

        public int UserId { get; }

        public int Recommend { get; }

        public override string GetSettingKey() => "Age_CustomerSelByAg";

        public override bool IsValid() => Recommend > 0;
    }
}
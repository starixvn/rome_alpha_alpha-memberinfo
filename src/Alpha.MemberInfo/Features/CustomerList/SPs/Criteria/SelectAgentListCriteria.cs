namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class SelectAgentListCriteria : SelectCustomerListCriteriaBase
    {
        public SelectAgentListCriteria(
            int userId,
            int mrecommend,
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
            MRecommend = mrecommend;
            UserId = userId;
        }

        public int UserId { get; }

        public int MRecommend { get; }

        public override string GetSettingKey() => "Acc_CustomerSelByMAg";

        public override bool IsValid() => MRecommend > 0;
    }
}

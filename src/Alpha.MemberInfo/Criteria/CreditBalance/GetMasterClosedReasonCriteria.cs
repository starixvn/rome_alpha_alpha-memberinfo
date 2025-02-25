namespace Alpha.MemberInfo.Criteria.CreditBalance
{
    public class GetMasterClosedReasonCriteria : GetCreditBalanceCriteriaBase
    {
        public GetMasterClosedReasonCriteria(
            int custId,
            string userName,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte levelType,
            int pageIndex,
            int pageSize)
            : base(custId, userName, enabled, closed, suspended, pageIndex, pageSize)
        {
            LevelType = levelType;
        }

        public byte LevelType { get; }

        public override string GetSettingKey()
            => "Age_CustomerSelCreditBalClosedReason_MAg";
    }
}

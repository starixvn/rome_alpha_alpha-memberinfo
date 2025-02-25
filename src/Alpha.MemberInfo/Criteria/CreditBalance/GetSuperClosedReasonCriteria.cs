namespace Alpha.MemberInfo.Criteria.CreditBalance
{
    public class GetSuperClosedReasonCriteria : GetMasterClosedReasonCriteria
    {
        public GetSuperClosedReasonCriteria(
            int custId,
            string userName,
            bool? enabled,
            bool? closed,
            bool? suspended,
            byte levelType,
            int pageIndex,
            int pageSize)
            : base(custId, userName, enabled, closed, suspended, levelType, pageIndex, pageSize)
        {
        }

        public override string GetSettingKey()
            => "Age_CustomerSelCreditBalClosedReason_SAg";
    }
}

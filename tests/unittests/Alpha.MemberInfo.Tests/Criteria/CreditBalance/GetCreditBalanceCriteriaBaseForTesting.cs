namespace Alpha.MemberInfo.Tests.Criteria.CreditBalance
{
    using System;
    using MemberInfo.Criteria.CreditBalance;

    public class GetCreditBalanceCriteriaBaseForTesting : GetCreditBalanceCriteriaBase
    {
        public GetCreditBalanceCriteriaBaseForTesting(
            int custId,
            string userName,
            bool? enabled,
            bool? closed,
            bool? suspended,
            int pageIndex,
            int pageSize)
            : base(custId, userName, enabled, closed, suspended, pageIndex, pageSize)
        {
        }

        public override string GetSettingKey()
        {
            throw new NotImplementedException();
        }
    }
}

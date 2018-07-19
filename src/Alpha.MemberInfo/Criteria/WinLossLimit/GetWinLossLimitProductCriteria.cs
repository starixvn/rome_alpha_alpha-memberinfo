namespace Alpha.MemberInfo.Criteria.WinLossLimit
{
    using Fanex.Data.Repository;

    public class GetWinLossLimitProductCriteria : CriteriaBase
    {
        public GetWinLossLimitProductCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey()
            => "Age_CustomerSelWinLimitProduct";

        public override bool IsValid()
            => CustId > 0;
    }
}

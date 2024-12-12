namespace Alpha.MemberInfo.Criteria.CreditBalance
{
    public class GetSuperCreditBalanceCriteria : GetPagedCreditBalanceCriteria
    {
        public GetSuperCreditBalanceCriteria(
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
            => "SiteDB_Age_CustomerSelCreditBalanceList_Sag";
    }
}

namespace Alpha.MemberInfo.Criteria.CreditBalance
{
    public class GetMasterCreditBalanceCriteria : GetPagedCreditBalanceCriteria
    {
        public GetMasterCreditBalanceCriteria(
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
            => "SiteDB_Age_CustomerSelCreditBalanceList_Mag";
    }
}

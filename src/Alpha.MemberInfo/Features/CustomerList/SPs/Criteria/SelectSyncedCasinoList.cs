namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Fanex.Data.Repository;

    public class SelectSyncedCasinoList : CriteriaBase
    {
        public SelectSyncedCasinoList(string custIds)
        {
            CustIds = custIds;
        }

        public string CustIds { get; }

        public override string GetSettingKey() => "Age_CustomerSelSyncCasino";

        public override bool IsValid() => !string.IsNullOrEmpty(CustIds);
    }
}

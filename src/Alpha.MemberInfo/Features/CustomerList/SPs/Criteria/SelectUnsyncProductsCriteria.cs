namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Fanex.Data.Repository;

    public class SelectUnsyncProductsCriteria : CriteriaBase
    {
        public SelectUnsyncProductsCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Acc_CustomerSelUnSyncProducts";

        public override bool IsValid() => CustId > 0;
    }
}
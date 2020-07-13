namespace Alpha.MemberInfo.Features.CustomerList.SPs.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectCustomerAllStatusCriteria : CriteriaBase
    {
        public SelectCustomerAllStatusCriteria(int searchAccountId) 
        {
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";

        public override bool IsValid() => SearchAccountId > 0;
    }
}

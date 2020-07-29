namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectCustomerAllStatusModelCriteria : CriteriaBase
    {
        public SelectCustomerAllStatusModelCriteria(int searchAccountId) 
        {
            SearchAccountId = searchAccountId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";

        public override bool IsValid() => SearchAccountId > 0;
    }
}

namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectCreditRangeCriteria : CriteriaBase
    {
        public SelectCreditRangeCriteria(string searchAccountName)
        {
            SearchAccountName = searchAccountName;
        }

        [SpParam(SqlParamName = "RefCust")]
        public string SearchAccountName { get; }

        public override string GetSettingKey() => "Age_CustomerInsMember_Ag_CreditSel";

        public override bool IsValid() => !string.IsNullOrEmpty(SearchAccountName);
    }
}

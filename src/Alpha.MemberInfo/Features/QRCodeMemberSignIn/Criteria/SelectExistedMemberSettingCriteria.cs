namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectExistedMemberSettingCriteria : CriteriaBase
    {
        public SelectExistedMemberSettingCriteria(int existedCustId)
        {
            ExistedCustId = existedCustId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int ExistedCustId { get; }

        public override string GetSettingKey() => "Acc_CustomerSelEditCustomer";

        public override bool IsValid() => ExistedCustId > 0 ;
    }
}

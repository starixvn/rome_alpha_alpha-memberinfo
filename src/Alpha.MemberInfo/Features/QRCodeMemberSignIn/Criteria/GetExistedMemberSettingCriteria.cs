namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetExistedMemberSettingCriteria : CriteriaBase
    {
        public GetExistedMemberSettingCriteria(int existedCustId)
        {
            ExistedCustId = existedCustId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int ExistedCustId { get; }

        public override string GetSettingKey() => "Acc_CustomerSelEditCustomer";

        public override bool IsValid() => ExistedCustId > 0 ;
    }
}

namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Criteria
{
    using Fanex.Data.Repository;

    public class GetCustomerProfileCriteria : CriteriaBase
    {
        public GetCustomerProfileCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }


        public override string GetSettingKey()
            => "Age_CustomerBehaviorProfile_Sel";

        public override bool IsValid()
            => CustId > 0;
    }
}

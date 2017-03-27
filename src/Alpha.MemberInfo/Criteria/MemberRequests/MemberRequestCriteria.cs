namespace Alpha.MemberInfo.Criteria.MemberRequests
{
    using Fanex.Data.Repository;

    public class MemberRequestCriteria : CriteriaBase
    {
        public MemberRequestCriteria(
            int custId,
            int productId)
        {
            CustId = custId;
            ProductId = productId;
        }

        public int CustId { get; }

        public int ProductId { get; }

        public override string GetSettingKey() => "Age_CustomerSelMemberRequestDetail";

        public override bool IsValid() => CustId > 0 && ProductId > 0;
    }
}
namespace Alpha.MemberInfo.Params
{
    public class MemberRequestsParam
    {
        public MemberRequestsParam(
            int custId,
            int productId)
        {
            CustId = custId;
            ProductId = productId;
        }

        public int CustId { get; }

        public int ProductId { get; }
    }
}
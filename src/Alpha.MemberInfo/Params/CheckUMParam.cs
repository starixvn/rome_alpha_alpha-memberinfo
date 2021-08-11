namespace Alpha.MemberInfo.Params
{
    public class CheckUMParam
    {
        public CheckUMParam(
            int custId,
            byte productId,
            string language)
        {
            CustId = custId;
            ProductId = productId;
            Language = language;
        }

        public int CustId { get; }

        public byte ProductId { get; }

        public string Language { get; set; }
    }
}
namespace Alpha.MemberInfo.Params
{
    public class ProgressiveJackpotParam
    {
        public ProgressiveJackpotParam(int custId, string language)
        {
            CustId = custId;
            Language = language;
        }

        public int CustId { get; }

        public string Language { get; }
    }
}
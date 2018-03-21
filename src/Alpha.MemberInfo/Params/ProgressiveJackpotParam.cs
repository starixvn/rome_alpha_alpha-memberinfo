using System.Collections.Generic;

namespace Alpha.MemberInfo.Params
{
    public class ProgressiveJackpotParam
    {
        public ProgressiveJackpotParam(int custId, string language, IEnumerable<int> productIds)
        {
            CustId = custId;
            Language = language;
            ProductIds = productIds;
        }

        public int CustId { get; }

        public IEnumerable<int> ProductIds { get; }

        public string Language { get; }
    }
}
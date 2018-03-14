using System.Collections.Generic;

namespace Alpha.MemberInfo.Params
{
    public class ProgressiveJackpotParam
    {
        public ProgressiveJackpotParam(int custId, string language, IList<int> productIds)
        {
            CustId = custId;
            Language = language;
            ProductIds = productIds;
        }

        public int CustId { get; }

        public IList<int> ProductIds { get; }

        public string Language { get; }
    }
}
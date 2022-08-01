namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params
{
    using System;

    public class SelectReachDetailParam
    {
        public SelectReachDetailParam(int custId, DateTime fromDate, DateTime toDate, byte limitType)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            LimitType = limitType;
        }

        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte LimitType { get; }
    }
}

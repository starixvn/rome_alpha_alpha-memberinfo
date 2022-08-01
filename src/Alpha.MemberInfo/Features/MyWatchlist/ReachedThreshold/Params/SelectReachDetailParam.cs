namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params
{
    using System;

    public class SelectReachDetailParam
    {
        public SelectReachDetailParam(
            int custId, 
            DateTime fromDate, 
            DateTime toDate, 
            byte limitType,
            DateTime limitedDateToShowInfo)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            LimitType = limitType;
            LimitedDateToShowInfo = limitedDateToShowInfo;
        }

        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte LimitType { get; }

        public DateTime LimitedDateToShowInfo { get; }
    }
}

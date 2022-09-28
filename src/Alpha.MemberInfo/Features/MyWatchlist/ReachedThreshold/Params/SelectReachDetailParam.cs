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
            byte sortType,
            bool isDesc,
            DateTime limitedDateToShowInfo)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            LimitType = limitType;
            SortType = sortType;
            IsDesc = isDesc;
            LimitedDateToShowInfo = limitedDateToShowInfo;
        }

        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte LimitType { get; }

        public byte SortType { get; }

        public bool IsDesc { get; }

        public DateTime LimitedDateToShowInfo { get; }
    }
}

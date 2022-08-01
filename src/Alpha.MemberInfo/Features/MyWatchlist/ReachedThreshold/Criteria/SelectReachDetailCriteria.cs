namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria
{
    using Fanex.Data.Repository;
    using System;

    public class SelectReachDetailCriteria : CriteriaBase
    {
        public SelectReachDetailCriteria(int custId, DateTime fromDate, DateTime toDate, byte limitType, int sporttype = 0)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            LimitType = limitType;
            Sporttype = sporttype;
        }

        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte LimitType { get; }

        public int Sporttype { get; }

        public override string GetSettingKey() => "Acc_WLMonitored_Player_ReachDetail_Sel";

        public override bool IsValid() => CustId > 0 && FromDate < ToDate;
    }
}

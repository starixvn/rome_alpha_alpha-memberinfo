namespace Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria
{
    using System;
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Fanex.Data.Repository;

    public class SelectMyThresholdHistoryCriteria : CriteriaBase
    {
        public SelectMyThresholdHistoryCriteria(SelectMyThresholdHistoryParam param)
        {
            CustId = param.CustId;
            SportType = param.SportType;
            FromDate = param.FromDate.GetGreaterDate(param.LimitedDateToShowInfo);
            ToDate = param.ToDate;
            SortType = param.SortType;
            IsDesc = param.IsDesc;
        }

        public int CustId { get; }

        public short SportType { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte SortType { get; }

        public bool IsDesc { get; }

        public override string GetSettingKey()
            => "Acc_WLMonitored_SMA_ReachDetail_Sel";

        public override bool IsValid()
            => CustId > 0 && FromDate <= ToDate;
    }
}

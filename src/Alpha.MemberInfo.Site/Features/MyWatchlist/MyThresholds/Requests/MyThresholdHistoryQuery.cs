namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests
{
    using System;
    using System.Collections.Generic;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class MyThresholdHistoryQuery : MemberInfoBaseQuery, IAsyncRequest<IEnumerable<ReachHistoryModel>>
    {
        public MyThresholdHistoryQuery(IUser viewer) : base(viewer)
        {
            LimitedDateToShowInfo = viewer.LimitedDateToShowInfo;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public byte SortType { get; set; }

        public bool IsDesc { get; set; }

        public DateTime LimitedDateToShowInfo { get; }

        public override bool IsValid()
            => base.IsValid() && FromDate <= ToDate;
    }
}
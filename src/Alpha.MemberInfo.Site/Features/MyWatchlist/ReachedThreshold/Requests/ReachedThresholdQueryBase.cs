namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Fanex.Common.Extensions;
    using System;
    using System.Web.Mvc;

    public class ReachedThresholdQueryBase : MemberInfoBaseQuery
    {
        public ReachedThresholdQueryBase(IUser viewer) : base(viewer)
        {
            LimitedDateToShowInfo = viewer.LimitedDateToShowInfo != default(DateTime);
        }

        [AllowHtml]
        public string Username { get; set; }

        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-1);

        public DateTime ToDate { get; set; } = DateTime.Today;

        public byte Status { get; set; }

        public byte LimitType { get; set; }

        public byte SortType { get; set; } = 1;

        public bool IsDesc { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        public bool LimitedDateToShowInfo { get; }

        public DateTime MinDate => DateTime.Today.AddMonths(-3).FirstDayOfMonth();

        public override bool IsValid()
            => base.IsValid() && FromDate <= ToDate && FromDate >= MinDate;
    }
}
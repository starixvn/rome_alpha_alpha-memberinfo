namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Fanex.Common.Extensions;
    using System;

    public class ReachedThresholdRequestBase : MemberInfoBaseQuery
    {
        public ReachedThresholdRequestBase(IUser viewer) : base(viewer)
        {
        }

        public string Username { get; set; }

        public DateTime FromDate { get; set; } = new DateTime(2022, 5, 1);

        public DateTime ToDate { get; set; } = new DateTime(2022, 5, 2);

        public byte Status { get; set; }

        public byte LimitType { get; set; }

        public byte SortType { get; set; } = 1;

        public bool IsDesc { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        public DateTime MinDate => DateTime.Today.AddMonths(-3).FirstDayOfMonth();

        public override bool IsValid()
            => base.IsValid() && FromDate <= ToDate && FromDate >= MinDate;
    }
}
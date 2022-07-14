namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using Fanex.Common.Extensions;
    using MediatR;
    using System;

    public class ReachHistoryQuery : MemberInfoBaseQuery, IAsyncRequest<ReachHistoryViewModel>
    {
        public ReachHistoryQuery(IUser viewer) : base(viewer)
        {
        }

        public int SelectedCustId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public byte LimitType { get; set; }

        public override bool IsValid()
            => base.IsValid() && FromDate <= ToDate && FromDate >= DateTime.Today.AddMonths(-3).FirstDayOfMonth();
    }
}
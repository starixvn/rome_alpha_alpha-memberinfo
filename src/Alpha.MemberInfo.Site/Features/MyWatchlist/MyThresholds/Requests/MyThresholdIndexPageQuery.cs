namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests
{
    using System;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.ViewModels;
    using Fanex.Common.Extensions;
    using MediatR;

    public class MyThresholdIndexPageQuery : MyThresholdHistoryQuery, IAsyncRequest<MyThresholdIndexPageViewModel>
    {
        public MyThresholdIndexPageQuery(IUser viewer) : base(viewer)
        {
            CurrentDate = DateTime.Today;
            FromDate = CurrentDate.AddDays(-1);
            ToDate = CurrentDate;
            MinDate = CurrentDate.AddMonths(-3).FirstDayOfMonth();
        }

        public DateTime CurrentDate { get; }

        public DateTime MinDate { get; }

        public override bool IsValid()
            => base.IsValid() && FromDate >= MinDate;
    }
}
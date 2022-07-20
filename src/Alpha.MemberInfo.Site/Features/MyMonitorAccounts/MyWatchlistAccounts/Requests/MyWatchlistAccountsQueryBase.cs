namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using System;

    public class MyWatchlistAccountsQueryBase : MemberInfoBaseQuery
    {
        public MyWatchlistAccountsQueryBase(IUser viewer) : base(viewer)
        {
            LimitedDateToShowInfo = viewer.LimitedDateToShowInfo != default(DateTime);
        }

        public string Username { get; set; }

        public byte SortType { get; set; } = 1;

        public bool IsDesc { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        public bool LimitedDateToShowInfo { get; }
    }
}
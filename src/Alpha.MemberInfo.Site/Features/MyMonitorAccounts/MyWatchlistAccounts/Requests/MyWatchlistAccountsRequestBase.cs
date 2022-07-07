namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;

    public class MyWatchlistAccountsRequestBase : MemberInfoBaseQuery
    {
        public MyWatchlistAccountsRequestBase(IUser viewer) : base(viewer)
        {
        }

        public string Username { get; set; }

        public byte SortType { get; set; } = 1;

        public bool IsDesc { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 100;
    }
}
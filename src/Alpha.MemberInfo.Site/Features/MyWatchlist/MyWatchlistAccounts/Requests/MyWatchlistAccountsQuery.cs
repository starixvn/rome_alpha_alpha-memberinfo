namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests
{
    using MediatR;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Models;
    using System;

    public class MyWatchlistAccountsQuery : MyWatchlistAccountsQueryBase, IAsyncRequest<MyWatchlistAccountsViewModel>
    {
        public MyWatchlistAccountsQuery(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-1);

        public DateTime ToDate { get; set; } = DateTime.Today;

        public byte Status { get; set; }

        public bool IsProblemAccount { get; set; }

        public bool IsOpenFromProblemAccount { get; set; }

        public string CustIds { get; set; }

        public IUser Viewer { get; }
    }
}
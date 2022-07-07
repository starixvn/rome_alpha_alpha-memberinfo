namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using System;

    public class WatchlistAccountViewModel
    {
        public WatchlistAccountViewModel(WatchlistAccountModel account)
        {
            RowNumber = account.RowNumber;
            CustId = account.CustId;
            Sporttype = account.Sporttype;
            Username = account.Username;
            LastLogged = account.LastLogged;
            Status = account.Status;
            WinningThreshold = account.WinningThreshold;
            LosingThreshold = account.LosingThreshold;

            LastLoggedString = account.LastLogged.ToLongDateTime12hWithHyphen();
            WinningThresholdString = ToMoneyFormat(account.WinningThreshold);
            LosingThresholdString = ToMoneyFormat(account.LosingThreshold);
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public DateTime LastLogged { get; }

        public string Status { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public string LastLoggedString { get; }

        public string WinningThresholdString { get; }

        public string LosingThresholdString { get; }

        private static string ToMoneyFormat(decimal? number)
            => number.HasValue 
            ? number.Value.ToMoneyFormat()
            : "Unlimited";
    }
}
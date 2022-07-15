namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models
{
    using System;

    public class WatchlistAccountModel
    {
        public WatchlistAccountModel()
        {
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public DateTime LastLogged { get; }

        public string Status { get; }

        public decimal? WinningThreshold { get; } 

        public decimal? LosingThreshold { get; }

        public byte ClosedReason { get; }

        public byte Locked { get; }
    }
}

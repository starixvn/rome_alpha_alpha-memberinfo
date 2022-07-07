namespace Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models
{
    using System;

    public class ReachlistAccountModel
    {
        public ReachlistAccountModel()
        {
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public DateTime LastLogged { get; }

        public DateTime LastReachedDate { get; }

        public string LastLimitTypeReach { get; }

        public string Status { get; }

        public decimal WinLossAmount { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public int ReachedCnt { get; }
    }
}

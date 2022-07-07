namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using System;

    public class ReachlistAccountViewModel
    {
        public ReachlistAccountViewModel(ReachlistAccountModel account)
        {
            RowNumber = account.RowNumber;
            CustId = account.CustId;
            Sporttype = account.Sporttype;
            Username = account.Username;
            LastLogged = account.LastLogged;
            LastReachedDate = account.LastReachedDate;
            LastLimitTypeReach = account.LastLimitTypeReach;
            WinLossAmount = account.WinLossAmount;
            ReachedCnt = account.ReachedCnt;
            Status = account.Status;
            WinningThreshold = account.WinningThreshold;
            LosingThreshold = account.LosingThreshold;

            LastLoggedString = account.LastLogged.ToLongDateTime12hWithHyphen();
            LastReachedDateString = account.LastReachedDate.ToLongDateTime12hWithHyphen();
            WinningThresholdString = ToMoneyFormat(account.WinningThreshold);
            LosingThresholdString = ToMoneyFormat(account.LosingThreshold);
            WinLossAmountString = account.WinLossAmount.ToMoneyFormat();
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

        public string LastLoggedString { get; }

        public string LastReachedDateString { get; }

        public string WinningThresholdString { get; }

        public string LosingThresholdString { get; }

        public string WinLossAmountString { get; }

        private static string ToMoneyFormat(decimal? number)
            => number.HasValue
            ? number.Value.ToMoneyFormat()
            : "Unlimited";
    }
}
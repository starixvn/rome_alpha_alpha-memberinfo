namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Models;
    using System;

    public class ReachlistAccountViewModel
    {
        public ReachlistAccountViewModel(ReachlistAccountModel account, bool limitedDateToShowInfo)
        {
            RowNumber = account.RowNumber;
            CustId = account.CustId;
            Sporttype = account.Sporttype;
            Username = account.Username;
            LastLimitTypeReach = Resources.ResourceManager.GetString(account.LastLimitTypeReach);
            WinLossAmount = account.WinLossAmmount;
            ReachedCnt = account.ReachCnt;
            Status = account.Status;
            WinningThreshold = account.WinningThreshold;
            LosingThreshold = account.LosingThreshold;
            ClosedReason = account.ClosedReason;
            Locked = account.Locked;

            LastLoggedString = limitedDateToShowInfo ? string.Empty : account.LastLogged.ToLongDateTime12hWithHyphen();
            LastReachedDateString = account.LastReachedDate.ToLongDateTime12hWithHyphen();
            WinningThresholdString = ToMoneyFormat(account.WinningThreshold);
            LosingThresholdString = ToMoneyFormat(account.LosingThreshold);
            WinLossAmountString = account.WinLossAmmount.ToMoneyFormat();
            TranslatedStatus = Resources.ResourceManager.GetString(account.Status);
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public string LastLimitTypeReach { get; }

        public string Status { get; }

        public string TranslatedStatus { get; }

        public decimal WinLossAmount { get; }

        public decimal? WinningThreshold { get; }

        public byte ClosedReason { get; }

        public byte Locked { get; }

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
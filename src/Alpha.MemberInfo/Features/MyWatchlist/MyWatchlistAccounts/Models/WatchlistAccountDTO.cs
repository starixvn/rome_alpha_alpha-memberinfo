namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Extensions;
    using System;

    public class WatchlistAccountDTO
    {
        public WatchlistAccountDTO(
            WatchlistAccountModel account,
            bool limitedDateToShowInfo)
        {
            RowNumber = account.RowNumber;
            CustId = account.CustId;
            Sporttype = account.Sporttype;
            Username = account.Username;
            Status = account.Status;
            WinningThreshold = account.WinningThreshold;
            LosingThreshold = account.LosingThreshold;
            ClosedReason = account.ClosedReason;
            Locked = account.Locked;

            LastLoggedString = limitedDateToShowInfo ? string.Empty : account.LastLogged.ToLongDateTime12hWithHyphen();
            WinningThresholdString = account.WinningThreshold.ToMoneyFormatWithUnlimited();
            LosingThresholdString = account.LosingThreshold.ToMoneyFormatWithUnlimited();
            TranslatedStatus = AppResources.Resources.ResourceManager.GetString(account.Status);
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public string Status { get; }

        public string TranslatedStatus { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public byte ClosedReason { get; }

        public byte Locked { get; }

        public string LastLoggedString { get; }

        public string WinningThresholdString { get; }

        public string LosingThresholdString { get; }
    }
}
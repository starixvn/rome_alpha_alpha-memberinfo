namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Extensions;
    using System;

    public class WatchlistAccountModel
    {
        public WatchlistAccountModel()
        {
        }

        public WatchlistAccountModel(
            int rowNumber,
            int custId,
            int sporttype, 
            string username, 
            DateTime lastLogged, 
            string status, 
            decimal? winningThreshold, 
            decimal? losingThreshold, 
            byte closedReason, 
            byte locked)
        {
            RowNumber = rowNumber;
            CustId = custId;
            Sporttype = sporttype;
            Username = username;
            LastLogged = lastLogged;
            Status = status;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
            ClosedReason = closedReason;
            Locked = locked;
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public DateTime LastLogged { get; internal set; }

        public string LastLoggedString => LastLogged.ToLongDateTime12hWithHyphen();

        public string Status { get; internal set; }

        public string TranslatedStatus => Resources.ResourceManager.GetString(Status);

        public decimal? WinningThreshold { get; internal set; } 

        public string WinningThresholdString => WinningThreshold.ToMoneyFormatWithUnlimited();

        public decimal? LosingThreshold { get; internal set; }

        public string LosingThresholdString => LosingThreshold.ToMoneyFormatWithUnlimited();

        public byte ClosedReason { get; }

        public byte Locked { get; }
        public bool isExistedThreshold { get; }
    }
}

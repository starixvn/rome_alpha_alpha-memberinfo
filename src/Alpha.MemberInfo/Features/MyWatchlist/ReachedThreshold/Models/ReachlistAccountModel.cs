namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Extensions;
    using System;

    public class ReachlistAccountModel
    {
        public ReachlistAccountModel()
        {
        }

        public ReachlistAccountModel(
            int rowNumber,
            int custId,
            int sporttype,
            string username,
            DateTime lastLogged,
            DateTime lastReachedDate,
            string lastLimitTypeReach,
            string status,
            decimal winLossAmount,
            decimal? winningThreshold,
            decimal? losingThreshold,
            int reachedCnt,
            byte closedReason,
            byte locked,
            int winningReachedCnt,
            int losingReachedCnt,
            bool isPA)
        {
            RowNumber = rowNumber;
            CustId = custId;
            Sporttype = sporttype;
            Username = username;
            LastLogged = lastLogged;
            LastReachedDate = lastReachedDate;
            LastLimitTypeReach = lastLimitTypeReach;
            Status = status;
            WinLossAmount = winLossAmount;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
            ReachedCnt = reachedCnt;
            ClosedReason = closedReason;
            Locked = locked;
            WinningReachedCnt = winningReachedCnt;
            LosingReachedCnt = losingReachedCnt;
            IsPA = isPA;
        }

        public int RowNumber { get; }

        public int CustId { get; }

        public int Sporttype { get; }

        public string Username { get; }

        public DateTime LastLogged { get; internal set; }

        public string LastLoggedString => LastLogged.ToLongDateTime12hWithHyphen();

        public DateTime LastReachedDate { get; internal set; }

        public string LastReachedDateString => LastReachedDate.ToLongDateTime12hWithHyphen();

        public string LastLimitTypeReach { get; internal set; }

        public string TranslatedLastLimitTypeReach => Resources.ResourceManager.GetString(LastLimitTypeReach);

        public string Status { get; internal set; }

        public string TranslatedStatus => Resources.ResourceManager.GetString(Status);

        public bool IsOpen => Status.Equals("Open", StringComparison.OrdinalIgnoreCase);

        public bool IsSuspended => Status.Equals(Resources.Suspended, StringComparison.OrdinalIgnoreCase);

        public decimal WinLossAmount { get; internal set; }

        public string WinLossAmountString => WinLossAmount.ToMoneyFormat();

        public decimal? WinningThreshold { get; internal set; }

        public string WinningThresholdString => WinningThreshold.ToMoneyFormatWithUnlimited();

        public decimal? LosingThreshold { get; internal set; }

        public string LosingThresholdString => LosingThreshold.ToMoneyFormatWithUnlimited();

        public int ReachedCnt { get; }

        public byte ClosedReason { get; }

        public byte Locked { get; }

        public int WinningReachedCnt { get; }

        public int LosingReachedCnt { get; }

        public string ReachedCountString
        {
            get
            {
                var winOrLoseText = string.Empty;

                if (WinningReachedCnt > 0 && LosingReachedCnt > 0)
                {
                    winOrLoseText = $"({WinningReachedCnt}W, {LosingReachedCnt}L)";
                }
                else if (WinningReachedCnt > 0)
                {
                    winOrLoseText = $"({WinningReachedCnt}W)";
                }
                else if (LosingReachedCnt > 0)
                {
                    winOrLoseText = $"({ LosingReachedCnt}L)";
                }

                return $"{ReachedCnt} {winOrLoseText}";
            }
        }

        public bool IsPA { get; }

        public string IsPAText => IsPA ? Resources.Yes : Resources.No2;
    }
}

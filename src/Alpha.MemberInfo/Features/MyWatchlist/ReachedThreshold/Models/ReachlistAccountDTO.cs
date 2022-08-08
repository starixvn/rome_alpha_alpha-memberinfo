namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Extensions;

    public class ReachlistAccountDTO
    {
        public ReachlistAccountDTO(ReachlistAccountModel account, bool isShowLastLoginColumn)
        {
            RowNumber = account.RowNumber;
            CustId = account.CustId;
            Sporttype = account.Sporttype;
            Username = account.Username;
            LastLimitTypeReach = Resources.ResourceManager.GetString(account.LastLimitTypeReach);
            WinLossAmount = account.WinLossAmount;
            ReachedCnt = account.ReachedCnt;
            Status = account.Status;
            WinningThreshold = account.WinningThreshold;
            LosingThreshold = account.LosingThreshold;
            ClosedReason = account.ClosedReason;
            Locked = account.Locked;

            LastLoggedString = isShowLastLoginColumn ? account.LastLogged.ToLongDateTime12hWithHyphen() : string.Empty;
            LastReachedDateString = account.LastReachedDate.ToLongDateTime12hWithHyphen();
            WinningThresholdString = account.WinningThreshold.ToMoneyFormatWithUnlimited();
            LosingThresholdString = account.LosingThreshold.ToMoneyFormatWithUnlimited();
            WinLossAmountString = account.WinLossAmount.ToMoneyFormat();
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
    }
}

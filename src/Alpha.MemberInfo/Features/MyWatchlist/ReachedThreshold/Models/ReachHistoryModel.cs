namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Extensions;
    using System;
    public class ReachHistoryModel
    {
        public ReachHistoryModel()
        {
        }

        public ReachHistoryModel(
            DateTime createdDate,
            string limitTypeReach,
            decimal winLossAmount,
            decimal? winningThreshold,
            decimal? losingThreshold,
            byte reachType)
        {
            CreatedDate = createdDate;
            LimitTypeReach = limitTypeReach;
            WinLossAmount = winLossAmount;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
            ReachType = reachType;
        }

        public int RowNumber { get; }

        public DateTime CreatedDate { get; internal set; }

        public string CreatedDateString => CreatedDate.ToLongDateTime12hWithHyphen();

        public string LimitTypeReach { get; internal set; }

        public string TranslatedLimitTypeReach
            => string.IsNullOrWhiteSpace(LimitTypeReach)
            ? string.Empty
            : Resources.ResourceManager.GetString(LimitTypeReach);

        public decimal WinLossAmount { get; internal set; }

        public string WinLossAmountString => WinLossAmount.ToMoneyFormat();

        public decimal? WinningThreshold { get; internal set; }

        public string WinningThresholdString => WinningThreshold.ToMoneyFormatWithUnlimited();

        public decimal? LosingThreshold { get; internal set; }

        public string LosingThresholdString => LosingThreshold.ToMoneyFormatWithUnlimited();

        public byte ReachType { get; }
    }
}

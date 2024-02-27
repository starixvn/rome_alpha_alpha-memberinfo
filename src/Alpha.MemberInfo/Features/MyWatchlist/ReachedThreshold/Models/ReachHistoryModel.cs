namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using System;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Extensions;

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

        public int RowNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedDateString => CreatedDate.ToLongDateTime12hWithHyphen();

        public string LimitTypeReach { get; set; }

        public string TranslatedLimitTypeReach
            => string.IsNullOrWhiteSpace(LimitTypeReach)
            ? string.Empty
            : Resources.ResourceManager.GetString(LimitTypeReach);

        public decimal WinLossAmount { get; set; }

        public string WinLossAmountString => WinLossAmount.ToMoneyFormat();

        public decimal? WinningThreshold { get; set; }

        public string WinningThresholdString => WinningThreshold.ToMoneyFormatWithUnlimited();

        public decimal? LosingThreshold { get; set; }

        public string LosingThresholdString => LosingThreshold.ToMoneyFormatWithUnlimited();

        public byte ReachType { get; set; }
    }
}

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
            decimal? losingThreshold)
        {
            CreatedDate = createdDate;
            LimitTypeReach = limitTypeReach;
            WinLossAmount = winLossAmount;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
        }

        public int RowNumber { get; }

        public DateTime CreatedDate { get; internal set; }

        public string CreatedDateString => CreatedDate.ToLongDateTime12hWithHyphen();

        public string LimitTypeReach { get; internal set; }

        public string TranslatedLimitTypeReach => Resources.ResourceManager.GetString(LimitTypeReach);

        public decimal WinLossAmount { get; internal set; }

        public string WinLossAmountString => WinLossAmount.ToMoneyFormat();

        public decimal? WinningThreshold { get; internal set; }

        public string WinningThresholdString => WinningThreshold.ToMoneyFormatWithUnlimited();

        public decimal? LosingThreshold { get; internal set; }

        public string LosingThresholdString => LosingThreshold.ToMoneyFormatWithUnlimited();

    }
}

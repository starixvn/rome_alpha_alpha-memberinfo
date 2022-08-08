namespace Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models
{
    using Alpha.MemberInfo.Extensions;

    public class ReachHistoryDTO
    {
        public ReachHistoryDTO(ReachHistoryModel reachHistoryModel, int rowNumber)
        {
            RowNumber = rowNumber;
            CreatedDateString = reachHistoryModel.CreatedDate.ToLongDateTime12hWithHyphen();
            LimitTypeReach = reachHistoryModel.LimitTypeReach;
            WinLossAmount = reachHistoryModel.WinLossAmount.ToMoneyFormat();
            WinningThresholdString = reachHistoryModel.WinningThreshold.ToMoneyFormatWithUnlimited();
            LosingThresholdString = reachHistoryModel.LosingThreshold.ToMoneyFormatWithUnlimited();
        }

        public int RowNumber { get; }

        public string CreatedDateString { get; }

        public string LimitTypeReach { get; }

        public string WinLossAmount { get; }

        public string WinningThresholdString { get; }

        public string LosingThresholdString { get; }
    }
}

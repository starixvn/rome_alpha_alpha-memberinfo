namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    public class BettingStatisticExportRow
    {
        public BettingStatisticExportRow() { }

        public string Sporttype { get; set; }

        public string Bettype { get; set; }

        public int WinBetCount { get; set; }

        public int LoseBetCount { get; set; }

        public int DrawBetCount { get; set; }

        public int TotalBetCount { get; set; }

        public decimal WinningWinLoss { get; set; }

        public decimal LosingWinLoss { get; set; }

        public decimal TotalWinLoss { get; set; }

        public decimal Margin { get; set; }

        public decimal AverageTurnover { get; set; }

        public decimal LiveTurnover { get; set; }

        public decimal NonLiveTurnover { get; set; }

        public decimal MixTurnover { get; set; }

        public decimal Turnover { get; set; }

        public decimal NetTurnOver { get; set; }
    }
}

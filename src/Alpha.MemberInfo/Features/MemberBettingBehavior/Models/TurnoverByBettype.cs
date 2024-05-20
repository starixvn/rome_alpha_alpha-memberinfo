namespace Alpha.MemberInfo.Features.MemberBettingBehavior.Models
{
    public class TurnoverByBettype
    {
        public TurnoverByBettype() { }

        public string Bettype { get; set; }

        public string ProductID { get; set; }

        public decimal LiveTurnover { get; set; }

        public decimal NonLiveTurnover { get; set; }

        public decimal MixTurnover { get; set; }
    }
}

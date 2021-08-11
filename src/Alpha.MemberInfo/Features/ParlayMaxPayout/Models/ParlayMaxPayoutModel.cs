namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Models
{
    public class ParlayMaxPayoutModel
    {
        public byte SportTypeId { get; }

        public byte BetTypeId { get; }

        public decimal Combo2 { get; }

        public decimal Combo35 { get; }

        public decimal ComboOver6 { get; }

        public decimal SingleMatchParlay { get; }

        public decimal UplineCombo2 { get; }

        public decimal UplineCombo35 { get; }

        public decimal UplineComboOver6 { get; }

        public decimal UplineSingleMatchParlay { get; }

        public bool IsAllowUnlimitCombo2 { get; }

        public bool IsAllowUnlimitCombo35 { get; }

        public bool IsAllowUnlimitComboOver6 { get; }

        public bool IsAllowUnlimitSingleMatchParlay { get; }
    }
}
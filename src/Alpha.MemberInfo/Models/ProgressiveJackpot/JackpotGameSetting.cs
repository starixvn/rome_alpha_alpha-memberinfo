namespace Alpha.MemberInfo.Models.ProgressiveJackpot
{
    public class JackpotGameSetting
    {
        private const string TwoDecimalPointsFormat = "{0:#,##0.00}";

        public JackpotGameSetting(
            string gameTypeName,
            decimal cbRate,
            decimal betStake)
        {
            GameTypeName = gameTypeName;
            CBRate = cbRate;
            BetStake = betStake;
        }

        public string GameTypeName { get; }

        public decimal CBRate { get; }

        public decimal BetStake { get; }

        public string CBRatePercent => string.Format(TwoDecimalPointsFormat, CBRate * 100) + " %";
    }
}
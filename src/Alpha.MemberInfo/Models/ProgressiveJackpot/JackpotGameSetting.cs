namespace Alpha.MemberInfo.Models.ProgressiveJackpot
{
    public class JackpotGameSetting
    {
        private const string TwoDecimalPointsFormat = "{0:#,##0.00}";

        public JackpotGameSetting(
            string gameTypeName,
            decimal contributionRate,
            decimal betStake)
        {
            GameTypeName = gameTypeName;
            ContributionRate = contributionRate;
            BetStake = betStake;
        }

        public string GameTypeName { get; }

        public decimal ContributionRate { get; }

        public decimal BetStake { get; }

        public string ContributionRatePercent => string.Format(TwoDecimalPointsFormat, ContributionRate * 100) + " %";
    }
}
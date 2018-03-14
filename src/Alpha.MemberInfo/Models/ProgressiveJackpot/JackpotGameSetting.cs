namespace Alpha.MemberInfo.Models.ProgressiveJackpot
{
    public class JackpotGameSetting
    {
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
    }
}
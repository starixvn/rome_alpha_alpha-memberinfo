namespace Alpha.MemberInfo.Models.ProgressiveJackpot
{
    public class JackpotGameSetting
    {
        public JackpotGameSetting(
            string gameName,
            decimal cbRate,
            decimal betStake)
        {
            GameName = gameName;
            CBRate = cbRate;
            BetStake = betStake;
        }

        public string GameName { get; }

        public decimal CBRate { get; }

        public decimal BetStake { get; }
    }
}
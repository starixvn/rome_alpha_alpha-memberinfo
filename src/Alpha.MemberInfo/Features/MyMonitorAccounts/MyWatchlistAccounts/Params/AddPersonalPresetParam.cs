namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params
{
    public class AddPersonalPresetParam
    {
        public AddPersonalPresetParam(int agentId, string presetTitle, decimal winningThreshold, decimal losingThreshold)
        {
            AgentId = agentId;
            PresetTitle = presetTitle;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
        }

        public int AgentId { get; }

        public string PresetTitle { get; }

        public decimal WinningThreshold { get; }

        public decimal LosingThreshold { get; }
    }
}
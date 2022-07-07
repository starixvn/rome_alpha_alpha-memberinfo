namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params
{
    public class SelectGlobalThresholdParam
    {
        public SelectGlobalThresholdParam(int agentId)
        {
            AgentId = agentId;
        }

        public int AgentId { get; }
    }
}

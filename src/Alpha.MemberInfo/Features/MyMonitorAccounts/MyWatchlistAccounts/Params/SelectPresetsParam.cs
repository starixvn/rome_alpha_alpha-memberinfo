namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params
{
    public class SelectPresetsParam
    {
        public SelectPresetsParam(int agentId)
        {
            AgentId = agentId;
        }

        public int AgentId { get; }
    }
}

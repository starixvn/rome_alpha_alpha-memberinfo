namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params
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

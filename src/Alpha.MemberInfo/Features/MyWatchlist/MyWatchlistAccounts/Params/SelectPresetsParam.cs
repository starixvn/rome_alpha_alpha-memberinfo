namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params
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

namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params
{
    public class DeletePersonalPresetParam
    {
        public DeletePersonalPresetParam(int id, int agentId)
        {
            ID = id;
            AgentId = agentId;
        }

        public int ID { get; }

        public int AgentId { get; }
    }
}

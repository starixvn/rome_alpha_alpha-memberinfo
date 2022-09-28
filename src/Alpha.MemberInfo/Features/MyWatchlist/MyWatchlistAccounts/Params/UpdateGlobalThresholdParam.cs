namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params
{
    public class UpdateGlobalThresholdParam
    {
        public UpdateGlobalThresholdParam(
           int agentId,
           decimal? winningThreshold,
           decimal? losingThreshold,
           int userId,
           int subAccId,
           string ip)
        {
            AgentId = agentId;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
            UserId = userId;
            SubAccId = subAccId;
            IP = ip;
        }

        public int AgentId { get; }


        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string IP { get; }
    }
}

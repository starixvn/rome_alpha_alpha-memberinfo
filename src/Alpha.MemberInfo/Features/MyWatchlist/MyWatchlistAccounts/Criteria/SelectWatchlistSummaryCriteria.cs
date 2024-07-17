namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Fanex.Data.Repository;

    public class SelectWatchlistSummaryCriteria : CriteriaBase
    {
        public SelectWatchlistSummaryCriteria(
            int agentId,
            int reachedDailyLimitCount,
            int sportType)
        {
            AgentId = agentId;
            ReachedDailyLimitCount = reachedDailyLimitCount;
            SportType = sportType;
        }

        public int AgentId { get; }

        public int ReachedDailyLimitCount { get; }

        public int SportType { get; }

        public override string GetSettingKey() => "Acc_WLMonitored_Player_WatchListSummary_Sel";

        public override bool IsValid() => AgentId > 0;
    }
}

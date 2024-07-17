namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using System;
    using Fanex.Data.Repository;

    public class SelectReachedMemberCountCriteria : NonQueryCommand
    {
        public SelectReachedMemberCountCriteria(
            int agentId,
            int sportType,
            DateTime fromDate,
            DateTime toDate,
            byte limitType)
        {
            AgentId = agentId;
            SportType = sportType;
            FromDate = fromDate;
            ToDate = toDate;
            LimitType = limitType;
        }

        public int AgentId { get; }

        public int SportType { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public byte LimitType { get; }

        public int ReachedDailyLimitCount { get; set; }

        public override string GetSettingKey() => "Acc_WLMonitored_Player_ReachedMembersCount_ByAg";

        public override bool IsValid() => AgentId > 0;
    }
}

namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Criteria
{
    using Fanex.Data.Repository;

    public class UpdateGlobalThresholdCommand : NonQueryCommand
    {
        public UpdateGlobalThresholdCommand(
            int agentId,
            decimal? winningThreshold,
            decimal? losingThreshold,
            int userId,
            int subAccId,
            string iP,
            int sporttype = 0)
        {
            AgentId = agentId;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
            UserId = userId;
            SubAccId = subAccId;
            IP = iP;
            Sporttype = sporttype;
        }

        public int AgentId { get; }

        public int Sporttype { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Acc_WLMonitored_Global_Adjust";

        public override bool IsValid() => AgentId > 0;
    }
}

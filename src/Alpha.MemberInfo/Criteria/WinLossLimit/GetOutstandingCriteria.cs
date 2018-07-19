namespace Alpha.MemberInfo.Criteria.WinLossLimit
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Params;

    public class GetOutstandingCriteria : CriteriaBase
    {
        public GetOutstandingCriteria(WinLossLimitParam param, int? playerId, int minCustId, int maxCustId)
        {
            PlayerId = playerId;

            CustId = param.CustId;
            ProductId = param.ProductId;
            BetType = param.BetType;
            SportType = param.SportType;
            Username = param.Username;
            MasterId = param.MasterId;
            AgentId = param.AgentId;
            MinCustId = minCustId;
            MaxCustId = maxCustId;
        }

        public int ProductId { get; set; }

        public int CustId { get; set; }

        public int MinCustId { get; set; }

        public int MaxCustId { get; set; }

        public int BetType { get; set; }

        public int SportType { get; set; }

        public string Username { get; set; }

        [SpParam(SqlParamName = "Mrecommend")]
        public int MasterId { get; set; }

        [SpParam(SqlParamName = "Recommend")]
        public int AgentId { get; set; }

        [SpParam(SqlParamName = "PlayerId_IBC")]
        public int? PlayerId { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerSelWinlimit_Outstanding";

        public override bool IsValid()
            => CustId > 0;
    }
}

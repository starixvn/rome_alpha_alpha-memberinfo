namespace Alpha.MemberInfo.Criteria.WinLossLimit
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Params;

    public class GetCustWinLossLimitCriteria : CriteriaBase
    {
        public GetCustWinLossLimitCriteria(WinLossLimitParam param, int? playerId)
        {
            PlayerId = playerId;

            CustId = param.CustId;
            ProductId = param.ProductId;
            BetType = param.BetType;
            SportType = param.SportType;
            Username = param.Username;
            MasterId = param.MasterId;
            AgentId = param.AgentId;
            PageSize = param.PageSize;
            PageIndex = param.PageIndex;
        }

        public int ProductId { get; set; }

        public int CustId { get; set; }

        public int BetType { get; set; }

        public int SportType { get; set; }

        public string Username { get; set; }

        [SpParam(SqlParamName = "Mrecommend")]
        public int MasterId { get; set; }

        [SpParam(SqlParamName = "Recommend")]
        public int AgentId { get; set; }

        [SpParam(SqlParamName = "PlayerId_IBC")]
        public int? PlayerId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerSelCustWinlimit";

        public override bool IsValid()
            => CustId > 0;
    }
}

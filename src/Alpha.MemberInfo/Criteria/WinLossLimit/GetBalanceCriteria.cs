namespace Alpha.MemberInfo.Criteria.WinLossLimit
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Params;

    public class GetBalanceCriteria : CriteriaBase
    {
        public GetBalanceCriteria(WinLossLimitParam param)
        {
            CustId = param.CustId;
            BetType = param.BetType;
            Username = param.Username;
            MasterId = param.MasterId;
            AgentId = param.AgentId;

            PageSize = param.PageSize;
            PageIndex = param.PageIndex;
        }

        public int CustId { get; set; }

        public int BetType { get; set; }

        public string Username { get; set; }

        [SpParam(SqlParamName = "Mrecommend")]
        public int MasterId { get; set; }

        [SpParam(SqlParamName = "Recommend")]
        public int AgentId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerSelWinlimitInfo";

        public override bool IsValid()
            => CustId > 0;
    }
}

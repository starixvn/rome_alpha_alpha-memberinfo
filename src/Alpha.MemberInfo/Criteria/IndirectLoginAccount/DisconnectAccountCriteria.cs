namespace Alpha.MemberInfo.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class DisconnectAccountCriteria : NonQueryCommand
    {
        public DisconnectAccountCriteria(DisconnectAccountParam param)
        {
            IndirectCustId = param.IndirectCustId;
            IndirectSubAccId = param.IndirectSubAccId;
            IndirectUsername = param.IndirectUsername;
            ActorId = param.ActorId;
            ActorSubAccId = param.ActorSubAccId;
            ActorUsername = param.ActorUsername;
            Language = param.LanguageKey;
            IP = param.IP;
        }

        [SpParam(SqlParamName = "custidindirect")]
        public int IndirectCustId { get; }

        [SpParam(SqlParamName = "subaccidindirect")]
        public int IndirectSubAccId { get; }

        [SpParam(SqlParamName = "downlineusername")]
        public string IndirectUsername { get; }

        [SpParam(SqlParamName = "custid")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "subaccid")]
        public int ActorSubAccId { get; }

        [SpParam(SqlParamName = "uplineusername")]
        public string ActorUsername { get; }

        public string IP { get; }

        public string Language { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerIndirectLoginAccountDel";

        public override bool IsValid()
            => IndirectCustId > 0
            && IndirectSubAccId >= 0
            && !string.IsNullOrWhiteSpace(IndirectUsername);
    }
}

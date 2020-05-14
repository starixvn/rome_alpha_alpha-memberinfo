namespace Alpha.MemberInfo.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class UpdatePasswordCriteria : NonQueryCommand
    {
        public UpdatePasswordCriteria(UpdatePasswordParam param)
        {
            IndirectCustId = param.IndirectCustId;
            IndirectSubAccId = param.IndirectSubAccId;
            IndirectUsername = param.IndirectUsername;
            IndirectLevelId = param.IndirectLevelId;
            UserPwd = param.Password;
            ActorId = param.ActorId;
            ActorSubAccId = param.ActorSubAccId;
            ActorUsername = param.ActorUsername;
            Language = param.LanguageKey;
            IP = param.RequestInfo.LoginIP;
            LoginDomain = param.RequestInfo.LoginDomain;
            BrowserName = param.RequestInfo.BrowserName;
            Platform = param.RequestInfo.Platform;
            CountryCode = param.RequestInfo.CountryCode;
        }

        [SpParam(SqlParamName = "custidindirect")]
        public int IndirectCustId { get; }

        [SpParam(SqlParamName = "subaccidindirect")]
        public int IndirectSubAccId { get; }

        [SpParam(SqlParamName = "downlineusername")]
        public string IndirectUsername { get; }

        [SpParam(SqlParamName = "roleidindirect")]
        public byte IndirectLevelId { get; }

        public string UserPwd { get; }

        [SpParam(SqlParamName = "custid")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "subaccid")]
        public int ActorSubAccId { get; }

        [SpParam(SqlParamName = "uplineusername")]
        public string ActorUsername { get; }

        public string IP { get; }

        public string Language { get; }

        public string LoginDomain { get; }

        public string BrowserName { get; }

        public string Platform { get; }

        public string CountryCode { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerIndirectLoginAccountPwdCheck";

        public override bool IsValid()
            => IndirectCustId > 0
            && IndirectSubAccId >= 0
            && !string.IsNullOrWhiteSpace(IndirectUsername)
            && IndirectLevelId > 0;
    }
}

namespace Alpha.MemberInfo.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class NewAccountCriteria : NonQueryCommand
    {
        public NewAccountCriteria(NewAccountParam param)
        {
            Username = param.CustName;
            UserPwd = param.Password;
            ActorId = param.ActorId;
            ActorSubAccId = param.ActorSubAccId;
            ActorLevelId = param.ActorLevelId;
            IP = param.RequestInfo.LoginIP;
            Language = param.LanguageKey;
            LoginDomain = param.RequestInfo.LoginDomain;
            BrowserName = param.RequestInfo.BrowserName;
            Platform = param.RequestInfo.Platform;
            CountryCode = param.RequestInfo.CountryCode;
        }

        public string Username { get; }

        public string UserPwd { get; }

        [SpParam(SqlParamName = "custid")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "subaccid")]
        public int ActorSubAccId { get; }

        [SpParam(SqlParamName = "roleid")]
        public byte ActorLevelId { get; }

        public string IP { get; }

        public string Language { get; }

        public string LoginDomain { get; }

        public string BrowserName { get; }

        public string Platform { get; }

        public string CountryCode { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Age_CustomerIndirectLoginAccountIns";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(Username)
            && !string.IsNullOrWhiteSpace(UserPwd)
            && ActorId > 0
            && ActorSubAccId >= 0;
    }
}

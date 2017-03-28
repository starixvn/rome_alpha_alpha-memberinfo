namespace Alpha.MemberInfo.Criteria.MemberRequests
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class EnableCasinoCommand : NonQueryCommand
    {
        public EnableCasinoCommand()
        {
        }

        public EnableCasinoCommand(
            int custId,
            string custName,
            int userId,
            int subAccId,
            string loginIP,
            string language)
        {
            CustId = custId;
            CustName = custName;
            UserId = userId;
            SubAccId = subAccId;
            LoginIP = loginIP;
            Language = language;
        }

        public int CustId { get; }

        [SpParam(Ignored = true)]
        public string CustName { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string LoginIP { get; }

        public string Language { get; }

        public bool DisabledCS => false;

        public bool Synccs => true;

        public bool EnableDownline => false;

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Age_UpdateCasinoStatus";

        public override bool IsValid() => CustId > 0 && UserId > 0 && !string.IsNullOrWhiteSpace(CustName);
    }
}
namespace Alpha.Reports.Criteria.MemberRequest
{
    using Fanex.Data.Repository;

    public class EnableCasinoCriteria : CriteriaBase
    {
        public EnableCasinoCriteria(
            int custId,
            int userId,
            int subAccId,
            string loginIP,
            string language)
        {
            CustId = custId;
            UserId = userId;
            SubAccId = subAccId;
            LoginIP = loginIP;
            Language = language;
        }

        public int CustId { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string LoginIP { get; }

        public string Language { get; }

        public bool DisabledCS => false;

        public bool Synccs => true;

        public bool EnableDownline => false;

        public override string GetSettingKey() => "Age_UpdateCasinoStatus";

        public override bool IsValid() => true;
    }
}
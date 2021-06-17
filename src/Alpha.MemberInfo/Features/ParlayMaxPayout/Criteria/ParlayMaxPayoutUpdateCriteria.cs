namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria
{
    using Fanex.Data.Repository;

    public class ParlayMaxPayoutUpdateCriteria : CriteriaBase
    {
        public ParlayMaxPayoutUpdateCriteria(
            int custId,
            string maxPayoutLimit,
            int userId,
            int subAccId,
            string language,
            string ip)
        {
            CustId = custId;
            MaxPayoutLimit = maxPayoutLimit;
            UserId = userId;
            SubAccId = subAccId;
            Language = language;
            Ip = ip;
        }

        public int CustId { get; }

        public string MaxPayoutLimit { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string Language { get; }

        public string Ip { get; }

        public override string GetSettingKey() => "Acc_CustSetting_MaxPayoutUpd";

        public override bool IsValid()
            => CustId > 0
            && UserId > 0
            && !string.IsNullOrWhiteSpace(MaxPayoutLimit);
    }
}
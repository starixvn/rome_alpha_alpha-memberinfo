namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria
{
    using Fanex.Data.Repository;

    public class ParlayMaxPayoutUpdateCriteria : CriteriaBase
    {
        private const byte alphaSystemId = 2;

        public ParlayMaxPayoutUpdateCriteria(
            int custId,
            string maxPayoutLimit,
            int subAccId,
            int userId,
            string ip)
        {
            CustId = custId;
            MaxPayoutLimit = maxPayoutLimit;
            SubAccId = subAccId;
            UserId = userId;
            Ip = ip;
            SystemId = alphaSystemId;
        }

        public int CustId { get; }

        public string MaxPayoutLimit { get; }

        public int SubAccId { get; }

        public int UserId { get; }

        public string Ip { get; }

        public byte SystemId { get; }

        public override string GetSettingKey() => "Acc_CustSetting_MaxPayoutUpd";

        public override bool IsValid()
            => CustId > 0
            && SubAccId > 0
            && UserId > 0
            && !string.IsNullOrWhiteSpace(MaxPayoutLimit);
    }
}
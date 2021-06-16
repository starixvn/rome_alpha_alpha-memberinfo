namespace Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria
{
    using Fanex.Data.Repository;

    public class ParlayMaxPayoutCheckDownlineCriteria : CriteriaBase
    {
        public ParlayMaxPayoutCheckDownlineCriteria(
            int custId,
            string maxPayoutLimit,
            string language)
        {
            CustId = custId;
            MaxPayoutLimit = maxPayoutLimit;
            Language = language;
        }

        public int CustId { get; }

        public string MaxPayoutLimit { get; }

        public string Language { get; }

        public override string GetSettingKey()
            => "Acc_CustSetting_MaxPayoutCheckDownline";

        public override bool IsValid()
            => CustId > 0 && !string.IsNullOrWhiteSpace(MaxPayoutLimit);
    }
}
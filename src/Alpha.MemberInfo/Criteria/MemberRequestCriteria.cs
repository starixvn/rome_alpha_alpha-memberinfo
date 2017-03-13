namespace Alpha.Reports.Criteria.WinLoss
{
    using Fanex.Data.Repository;

    public class MemberRequestCriteria : CriteriaBase
    {
        public MemberRequestCriteria()
        {
        }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => true;
    }
}
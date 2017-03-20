namespace Alpha.Reports.Criteria.MemberRequest
{
    using Fanex.Data.Repository;

    public class EnableProductCriteria : CriteriaBase
    {
        public EnableProductCriteria()
        {
        }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => true;
    }
}
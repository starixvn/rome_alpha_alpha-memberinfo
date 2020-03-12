namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectSuggestUsernameCriteria : CriteriaBase
    {
        public SelectSuggestUsernameCriteria(string actorName, int custId, byte roleId)
        {
            ActorName = actorName;
            CustId = custId;
            RoleId = 2;
        }

        [SpParam(SqlParamName = "Prefix")]
        public string ActorName { get; }

        public int CustId { get; }

        public byte RoleId { get; }

        public override string GetSettingKey() => "Age_CustomerSelSuggestUsername";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(ActorName);
    }
}

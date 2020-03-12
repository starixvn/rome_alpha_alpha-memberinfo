namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectAgentInfoCriteria : CriteriaBase
    {
        public SelectAgentInfoCriteria(int actorId)
        {
            ActorId = actorId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int ActorId { get; }

        public override string GetSettingKey() => "Acc_CustomerSelAgentInfo";

        public override bool IsValid() => ActorId > 0;
    }
}

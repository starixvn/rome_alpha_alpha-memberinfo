namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class UpdateMultipleDoubleCommCommand : CustomerStatusQueryBase, IRequest<IEnumerable<Result>>
    {
        public UpdateMultipleDoubleCommCommand(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid()
        => !string.IsNullOrEmpty(SearchAccountIds)
            && CanUpdateAccount
            && !IsInternal
            && ((ActorLevel.IsSuperLevel() && AgentId == DirectMemberAgentIds.Super) || (ActorLevel.IsMasterLevel() && AgentId == DirectMemberAgentIds.Master) || (viewer.LevelId == SearchAccountRoleId + 1));

        public string SearchAccountIds { get; set; }

        public bool EnabledDoubleComm { get; set; }

        public new int AgentId { get; set; }
    }
}
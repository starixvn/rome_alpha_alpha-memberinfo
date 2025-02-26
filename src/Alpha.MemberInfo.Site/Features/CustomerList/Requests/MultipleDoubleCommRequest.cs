namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class MultipleDoubleCommRequest : FetchDownlinesListRequesBase, IRequest<MultipleDoubleCommViewModel>
    {
        public MultipleDoubleCommRequest(IUser viewer) : base(viewer)
        {
        }

        public override bool IsValid() => !string.IsNullOrEmpty(SearchAccountIds) && (IsDirectMember || (viewer.LevelId == SearchAccountRoleId + 1));

        public string SearchAccountIds { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public int AgentId { get; set; }

        public bool IsDirectMember
            => (ActorLevel.IsSuperLevel() && AgentId == DirectMemberAgentIds.Super) || (ActorLevel.IsMasterLevel() && AgentId == DirectMemberAgentIds.Master);
    }
}
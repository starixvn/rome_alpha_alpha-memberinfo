namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class MasterListRequest : IRequest<MasterListViewModel>
    {
        public MasterListRequest(IUser viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.Name;
            ActorRoleId = viewer.LevelId;
            SearchAccountRoleId = viewer.LevelId;
        }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public byte SearchAccountRoleId { get; set; }
    }
}
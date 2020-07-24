namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class CustomerListRequest : IRequest<MasterListViewModel>
    {
        public CustomerListRequest(IUser viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.Name;
            ActorRoleId = viewer.LevelId;
            SearchAccountRoleId = viewer.LevelId;
            PageSize = 10;
            PageIndex = 1;
        }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public byte SearchAccountRoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
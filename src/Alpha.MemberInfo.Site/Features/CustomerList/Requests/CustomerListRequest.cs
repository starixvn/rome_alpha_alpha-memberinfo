namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class CustomerListRequest : IRequest<CustomerListViewModel>
    {
        public CustomerListRequest(IUser viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.Name;
            ActorRoleId = viewer.LevelId;

            SearchAccountId = viewer.Id;
            SearchAccountRoleId = viewer.LevelId;
            SearchAccountName = viewer.Name;
        }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }
    }
}
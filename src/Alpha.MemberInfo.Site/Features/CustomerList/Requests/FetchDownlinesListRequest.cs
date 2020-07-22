namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class FetchDownlinesListRequest : IRequest<CustomerListDTO>
    {
        public FetchDownlinesListRequest(IUser viewer)
        {
            ActorId = viewer.Id;
            SearchAccountId = viewer.Id;
            SearchAccountRoleId = viewer.LevelId;
            PageSize = 10;
            PageIndex = 1;
        }

        public int ActorId { get; }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
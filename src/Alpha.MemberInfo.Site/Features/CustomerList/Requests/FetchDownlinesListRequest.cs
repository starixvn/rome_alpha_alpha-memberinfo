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
            SearchAccountName = viewer.Name;

            PageSize = 10;
            PageIndex = 1;
        }

        public int ActorId { get; }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public string Username { get; set; }

        public byte Status { get; set; }

        public byte DoubleComm { get; set; }

        public byte UnconfirmSetting { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool? Suspended
           => Status.Equals(2) ? (bool?)true : null;

        public bool? Enabled
             => Status.Equals(4) ? (bool?)false : null;

        public bool? Closed
            => Status.Equals(1) || Status.Equals(3)? (bool?)Status.Equals(3) : null;
    }
}
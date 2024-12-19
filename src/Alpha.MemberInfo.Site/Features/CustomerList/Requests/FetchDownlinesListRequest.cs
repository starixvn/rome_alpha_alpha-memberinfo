namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class FetchDownlinesListRequest : FetchDownlinesListRequesBase, IRequest<CustomerListPagingViewModel>
    {
        public FetchDownlinesListRequest(IUser viewer) : base(viewer)
        {
        }

        public byte LevelType { get; set; }
    }
}
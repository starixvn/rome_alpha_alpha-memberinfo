namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;

    public class CustomerListRequest : MemberInfoBaseQuery, IRequest<CustomerListViewModel>
    {
        public CustomerListRequest(IUser viewer) : base(viewer)
        {
        }
    }
}
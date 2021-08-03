namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class CustomerListRequest : FetchDownlinesListRequesBase, IRequest<CustomerListViewModel>
    {
        public CustomerListRequest(IUser viewer) : base(viewer)
        {
            Viewer = viewer;
        }

        public IUser Viewer { get; }
    }
}
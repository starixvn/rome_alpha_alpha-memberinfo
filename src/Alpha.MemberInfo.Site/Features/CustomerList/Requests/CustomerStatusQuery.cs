namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class CustomerStatusQuery : CustomerStatusQueryBase, IAsyncRequest<CustomerStatusViewModel>
    {
        public CustomerStatusQuery(IUser viewer) : base(viewer)
        {
        }
    }
}
namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class MasterListRequest : IRequest<MasterListViewModel>
    {
        public MasterListRequest(IUser viewer)
        {
        }
    }
}
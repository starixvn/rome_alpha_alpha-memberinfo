namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;
    using System.Collections.Generic;

    public class MasterListRequest : IRequest<IEnumerable<MasterListViewModel>>
    {
        public MasterListRequest(IUser viewer)
        {
        }
    }
}
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests;
using MediatR;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.Request
{
    public class CheckExistedCustomerQuery : MemberInfoBaseQuery, IAsyncRequest<bool>
    {
        public CheckExistedCustomerQuery(IUser viewer) 
            : base(viewer)
        {
        }
    }
}
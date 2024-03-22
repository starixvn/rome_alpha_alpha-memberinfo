using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests;
using Fanex.Customer.Client.Models;
using MediatR;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.Request
{
    public class CreditQuery : MemberInfoBaseQuery, IAsyncRequest<CreditInfo>
    {
        public CreditQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}
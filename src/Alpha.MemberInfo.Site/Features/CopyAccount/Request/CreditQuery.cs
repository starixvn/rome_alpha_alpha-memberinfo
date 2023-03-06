using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount.Models;
using Alpha.MemberInfo.Site.Requests;
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
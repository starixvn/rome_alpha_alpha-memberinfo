using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests;
using MediatR;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.Request
{
    public class CheckUsernameQuery : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public CheckUsernameQuery(IUser viewer) 
            : base(viewer)
        {
        }
    }
}
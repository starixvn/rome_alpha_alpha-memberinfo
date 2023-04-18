using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests;
using MediatR;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;

namespace Alpha.MemberInfo.Site.Features.CopyAccount.Request
{
    public class CopyAccountQuery : MemberInfoBaseQuery, IAsyncRequest<CopyAccountViewModel>
    {
        public CopyAccountQuery(IUser viewer) 
            : base(viewer)
        {
        }
    }
}
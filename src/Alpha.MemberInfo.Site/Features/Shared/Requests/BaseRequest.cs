namespace Alpha.MemberInfo.Site.Features.Shared.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;

    public class BaseRequest : MemberInfoBaseQuery
    {
        protected BaseRequest(IUser viewer) : base (viewer)
        {
        }
    }
}
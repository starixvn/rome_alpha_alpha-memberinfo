namespace Alpha.MemberInfo.Site.Features.Shared.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;

    public class BaseRequest : MemberInfoBaseQuery
    {
        public BaseRequest(IUser viewer) : base(viewer)
        {
        }
    }
}
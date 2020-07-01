namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using System.Web;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class RedirectQuery : MemberInfoBaseQuery, IAsyncRequest<string>
    {
        public RedirectQuery(IUser viewer) : base(viewer)
        {
        }

        public string BrowserSize { get; set; }

        public string IBCPKey
            => HttpContext.Current.Server.MapPath("~/IbcKeys/ibcpkey.snk");

        public HttpRequest Request
            => HttpContext.Current.Request;
    }
}
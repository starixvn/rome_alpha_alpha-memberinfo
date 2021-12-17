namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using System.Web;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class RedirectQuery : MemberInfoBaseQuery, IAsyncRequest<string>
    {
        public RedirectQuery(IUser viewer) : base(viewer)
        {
            DarkModeName = viewer.DarkModeName;
            AssetAppPath = viewer.AssetAppPath;
        }

        public string BrowserSize { get; set; }

        public string IBCPKey
            => HttpContext.Current.Server.MapPath("~/IbcKeys/ibcpkey.snk");

        public HttpRequest Request
            => HttpContext.Current.Request;

        public string DarkModeName { get; }

        public string AssetAppPath { get; }
    }
}
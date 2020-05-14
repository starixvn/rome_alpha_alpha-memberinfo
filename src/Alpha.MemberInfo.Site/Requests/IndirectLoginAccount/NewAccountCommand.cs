namespace Alpha.MemberInfo.Site.Requests.IndirectLoginAccount
{
    using System.Web;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class NewAccountCommand : MemberInfoBaseQuery, IAsyncRequest<Result>
    {
        public NewAccountCommand(IUser viewer) : base(viewer)
        {
        }

        public string Password { get; set; }

        public string BrowserSize { get; set; }

        public HttpRequest Request => HttpContext.Current.Request;

        public override bool IsValid()
            => base.IsValid() && !string.IsNullOrWhiteSpace(Password) && !IsInternal;
    }
}
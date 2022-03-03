namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.WinLossLimit;

    public class WinLossLimitController : SiteBaseController
    {
        public WinLossLimitController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index(WinLossLimitQuery message)
            => View(message, "~/Views/WinLossLimit/Index.cshtml");
    }
}
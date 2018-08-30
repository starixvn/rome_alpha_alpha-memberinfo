namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.WinLossLimit;
    using Web;

    public class WinLossLimitController : BaseController
    {
        private readonly IMediator mediator;

        public WinLossLimitController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(WinLossLimitQuery message)
            => View(mediator.Send(message));
    }
}
namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.MemberRequest;
    using Web;

    [SessionState(SessionStateBehavior.Default)]
    public class MemberRequestsController : BaseController
    {
        private readonly IMediator mediator;

        public MemberRequestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult Index(ProductQuery message)
            => View(mediator.Send(message));

        [HttpGet]
        public ActionResult Members(MemberRequestsQuery message)
            => PartialView(mediator.Send(message));

        [HttpPost]
        public JsonResult EnableProduct(EnableProductCommand command)
            => Json(mediator.Send(command));
    }
}
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
        {
            var viewModel = mediator.Send(message);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Members(MemberRequestsQuery message)
        {
            var viewModel = mediator.Send(message);

            return PartialView(viewModel);
        }

        [HttpPost]
        public JsonResult EnableProduct(EnableProductCommand message)
        {
            var viewModel = mediator.Send(message);

            return Json(viewModel);
        }
    }
}
namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using Requests.MemberRequest;

    [SessionState(SessionStateBehavior.Default)]
    public class MemberRequestsController : SiteBaseController
    {
        public MemberRequestsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult Index(ProductQuery message)
            => View(message, "~/Views/MemberRequest/Index.cshtml");

        [HttpGet]
        public ActionResult Members(MemberRequestsQuery message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return PartialView(mediator.Send(message));
        }
    }
}
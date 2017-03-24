namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using AppResources;
    using MediatR;
    using MemberInfo.Models;
    using Requests.MemberRequest;
    using Web;

    [SessionState(SessionStateBehavior.Default)]
    public class MemberRequestsController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly IMediator mediator;

        public MemberRequestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult Index(ProductQuery message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(mediator.Send(message));
        }

        [HttpGet]
        public ActionResult Members(MemberRequestsQuery message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return PartialView(mediator.Send(message));
        }

        [HttpPost]
        public JsonResult EnableProduct(EnableProductCommand command)
        {
            if (command != null && !command.IsValid())
            {
                return Json(Result.CreateErrorResult(Resources.NoPermissionAccessPage, 101));
            }

            return Json(mediator.Send(command));
        }
    }
}
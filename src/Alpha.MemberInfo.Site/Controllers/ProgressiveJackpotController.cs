namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.ProgressiveJackpot;
    using Web;

    public class ProgressiveJackpotController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly IMediator mediator;

        public ProgressiveJackpotController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(ProgressiveJacpotQuery message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(mediator.Send(message));
        }
    }
}
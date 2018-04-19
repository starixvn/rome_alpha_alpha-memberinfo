namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using MemberInfo.Models;
    using Requests.ProgressiveJackpot;
    using Web;

    public class ProgressiveJackpotController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly IMediator mediator;
        private readonly IUser viewer;

        public ProgressiveJackpotController(IMediator mediator, IUser viewer)
        {
            this.mediator = mediator;
            this.viewer = viewer;
        }

        public ActionResult Index(ProgressiveJacpotQuery message)
            => message != null && message.IsValid()
            ? View(mediator.Send(message))
            : View(InvalidPageViewPath);

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!viewer.IsEnabledJackpot)
            {
                UnAuthentication(filterContext);
            }
        }
    }
}
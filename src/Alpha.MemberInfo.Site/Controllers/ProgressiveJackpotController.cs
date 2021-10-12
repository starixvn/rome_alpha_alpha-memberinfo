namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using MemberInfo.Models;
    using Requests.ProgressiveJackpot;

    public class ProgressiveJackpotController : SiteBaseController
    {
        private readonly IUser viewer;

        public ProgressiveJackpotController(IMediator mediator, IUser viewer)
            : base(mediator)
        {
            this.viewer = viewer;
        }

        public ActionResult Index(ProgressiveJacpotQuery message)
            => View(message, "~/Views/ProgressiveJackpot/Index.cshtml");

        public ActionResult Handbook()
            => View($"~/Views/ProgressiveJackpot/Handbook/{viewer.Language}.cshtml");

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

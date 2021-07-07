namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.Web;
    using Alpha.Web.Filters;
    using MediatR;
    using Requests;
    using System.Web.Mvc;

    public class ParlayMaxPayoutController : BaseController
    {
        private readonly IMediator mediator;

        private const string editSinglePath = "~/Features/ParlayMaxPayout/Views/EditSingle.cshtml";
        private const string editMultiplePath = "~/Features/ParlayMaxPayout/Views/EditMultiple.cshtml";

        public ParlayMaxPayoutController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult EditSingle(ParlayMaxPayoutSingleSelectQuery message)
            => View(editSinglePath, mediator.Send(message));

        [HttpPost]
        public ActionResult EditMultiple(ParlayMaxPayoutMultipleSelectQuery message)
            => View(editMultiplePath, mediator.Send(message));

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateSingle(ParlayMaxPayoutUpdateSingleQuery message)
            => Json(mediator.Send(message));

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateMultiple(ParlayMaxPayoutUpdateMultipleQuery message)
            => Json(mediator.Send(message));

        [HttpPost, AntiXsrfFilter]
        public ActionResult CheckDownline(ParlayMaxPayoutCheckDownlineQuery message)
            => Json(mediator.Send(message));
    }
}
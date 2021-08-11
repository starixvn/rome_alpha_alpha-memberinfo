namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.Web.Filters;
    using Controllers;
    using MediatR;
    using Requests;
    using System.Web.Mvc;

    public class ParlayMaxPayoutController : SiteBaseController
    {
        private const string editSinglePath = "~/Features/ParlayMaxPayout/Views/EditSingle.cshtml";
        private const string editMultiplePath = "~/Features/ParlayMaxPayout/Views/EditMultiple.cshtml";

        public ParlayMaxPayoutController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult EditSingle(ParlayMaxPayoutSingleSelectQuery message)
            => View(message, editSinglePath);

        [HttpPost]
        public ActionResult EditMultiple(ParlayMaxPayoutMultipleSelectQuery message)
            => View(message, editMultiplePath);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateSingle(ParlayMaxPayoutUpdateSingleQuery message)
            => Json(message);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateMultiple(ParlayMaxPayoutUpdateMultipleQuery message)
            => Json(message);

        [HttpPost, AntiXsrfFilter]
        public ActionResult CheckDownline(ParlayMaxPayoutCheckDownlineQuery message)
            => Json(message);
    }
}
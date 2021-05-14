namespace Alpha.MemberInfo.Site.Features.ParlayMaxPayout
{
    using Alpha.Web;
    using System.Web.Mvc;

    public class ParlayMaxPayoutController : BaseController
    {
        private const string editSinglePath = "~/Features/ParlayMaxPayout/Views/EditSingle.cshtml";
        private const string editMultiplePath = "~/Features/ParlayMaxPayout/Views/EditMultiple.cshtml";
        
        public ActionResult EditSingle() => View(editSinglePath);

        public ActionResult EditMultiple() => View(editMultiplePath);
    }
}
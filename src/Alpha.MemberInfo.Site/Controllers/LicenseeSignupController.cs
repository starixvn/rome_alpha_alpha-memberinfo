using System.Web.Mvc;
using Alpha.MemberInfo.Site.Requests.LicenseeSignup;
using Alpha.MemberInfo.Site.ViewModels.LicenseeSignup;
using MediatR;

namespace Alpha.MemberInfo.Site.Controllers
{
    public class LicenseeSignupController : SiteBaseController
    {
        public LicenseeSignupController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult Index(LicenseeSignupQuery query) => View(new LicenseeSignupViewModel(query));
    }
}
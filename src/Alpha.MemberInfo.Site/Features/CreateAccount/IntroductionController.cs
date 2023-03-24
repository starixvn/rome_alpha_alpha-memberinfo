using System.Web.Mvc;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels;
using Alpha.Web;

namespace Alpha.MemberInfo.Site.Features.CreateAccount
{
    public class IntroductionController : BaseController
    {
        private readonly IUser viewer;
        private const string QuickIntroductionPath = "~/Features/CreateAccount/Views/QuickIntroduction.cshtml";

        public IntroductionController(IUser viewer)
        {
            this.viewer = viewer;
        }
     
        public ActionResult QuickIntroduction()
            => View(QuickIntroductionPath, new QuickIntroductionViewModel(viewer.Name, viewer.Language));
    }
}
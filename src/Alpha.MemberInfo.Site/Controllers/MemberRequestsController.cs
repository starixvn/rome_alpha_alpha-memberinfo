namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using Requests.MemberRequest;

    public class MemberRequestsController : Controller
    {
        public ActionResult Index(MemberRequestQuery message) 
            => View();
    }
}
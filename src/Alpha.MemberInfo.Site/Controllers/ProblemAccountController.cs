namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.Web.Filters;
    using MediatR;

    [SessionState(SessionStateBehavior.Default)]
    public class ProblemAccountController : SiteBaseController
    {
        public ProblemAccountController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult Index(IndexRequest message)
            => View(message, "Index");

        [HttpGet]
        public ActionResult Update(UpdateProblemAccountRequest message)
            => View(message, "Update");

        public ActionResult GetProblemAccounts(GetProblemAccountRequest message)
            => Json(message);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCommand(UpdateProblemAccountCommand message)
            => Json(message);

        public ActionResult Excel(ProblemAccountExcelQuery message)
            => ExcelView(message);
    }
}
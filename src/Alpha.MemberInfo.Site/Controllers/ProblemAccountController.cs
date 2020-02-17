namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using Web;

    [SessionState(SessionStateBehavior.Default)]
    public class ProblemAccountController : BaseController
    {
        private const string InvalidExcelFileName = "Invalid";
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly IMediator mediator;

        public ProblemAccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(mediator.Send(message));
        }

        public ActionResult Update(UpdateProblemAccountRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(mediator.Send(message));
        }

        public ActionResult GetProblemAccounts(ProblemAccountRequest message)
            => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public ActionResult Update(UpdateProblemAccountCommand message)
            => Json(mediator.Send(message));

        public ActionResult Excel(ProblemAccountExcelQuery message)
            => message.IsValid()
                ? GetExcelContent(mediator.Send(message))
                : GetExcelContent(new ExcelFileViewModel(InvalidExcelFileName, new byte[] { }));

        private FileContentResult GetExcelContent(ExcelFileViewModel viewModel)
            => File(viewModel.Content, viewModel.ContentType, viewModel.Name);
    }
}
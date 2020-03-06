namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.Web;
    using Alpha.Web.Models;
    using MediatR;
    using System.Web.Mvc;

    public class QRCodeMemberSignInController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string IndexPath = "~/Features/QRCodeMemberSignIn/Views/Index.cshtml";
        private readonly IMediator mediator;

        public QRCodeMemberSignInController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult Index(QRCodeMemberSignInRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(IndexPath, mediator.Send(message));
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountByQRCodeRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return Json(new Result(1, "Forbidden"), JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(message));
        }

        [HttpGet]
        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public ActionResult GetMemberCredit(GetMemberCreditRequest message)
         => Json(mediator.Send(message));
    }
}
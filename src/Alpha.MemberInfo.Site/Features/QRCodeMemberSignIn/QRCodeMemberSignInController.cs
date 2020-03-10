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
        private const string ShowQRCodePath = "~/Features/QRCodeMemberSignIn/Views/ShowQRCode.cshtml";
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

            var result = mediator.Send(message);
            if(result.ErrCode.Equals(0))
            {
                return RedirectToAction("ShowQRCode", new {message.Username, message.FirstName, message.Credit });
            }

            return RedirectToAction("Index", new { MemberName = "MemberName", ErrorMessage="Create Account Failed" });
        }

        [HttpGet]
        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public ActionResult GetMemberCredit(GetMemberCreditRequest message)
         => Json(mediator.Send(message));

        [HttpGet]
        public ActionResult ShowQRCode(ShowQRCodeRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(ShowQRCodePath, mediator.Send(message));
        }
    }
}
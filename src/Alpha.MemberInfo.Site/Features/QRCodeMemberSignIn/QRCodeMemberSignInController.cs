namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.Captcha.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.Web;
    using MediatR;
    using System.Linq;
    using System.Web.Mvc;

    public class QRCodeMemberSignInController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string IndexPath = "~/Features/QRCodeMemberSignIn/Views/Index.cshtml";
        private const string ShowQRCodePath = "~/Features/QRCodeMemberSignIn/Views/ShowQRCode.cshtml";
        private const string Separator = "; ";
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

        private ActionResult RedirectToIndex(string memberName, string errorMessage)
        {
            return RedirectToAction("Index",
               routeValues: new
               {
                   MemberName = memberName,
                   ErrorMessage = errorMessage
               });
        }

        [HttpPost]
        [CaptchaValidation]
        public ActionResult CreateAccount(CreateAccountByQRCodeRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return RedirectToIndex(message.UsernameCopyFrom, Resources.UsernameIsInvalid );
            }

            if (!ModelState.IsValid)
            {
                return RedirectToIndex(
                    message.UsernameCopyFrom,
                    string.Join(Separator,
                    ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }

            var result = mediator.Send(message);

            if (!result.Success)
            {
                return RedirectToIndex(message.UsernameCopyFrom, result.ErrMsg);
            }

            return RedirectToAction("ShowQRCode",
                    routeValues: new
                    {
                        SearchAccountName = message.Username,
                        SearchAccountId = result.CustId,
                        message.FirstName,
                        message.Credit,
                        message.CreditMin,
                        message.CreditMax,
                        MemberNameCopyFrom = message.UsernameCopyFrom,
                        message.ShowPageTitle,
                        ShowMessage = true,
                        MemberIdCopyFrom = result.CustIdCopyFrom
                    });
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

        [HttpPost]
        public ActionResult GenerateQrCode(GenerateQRCodeRequest message)
         => Json(mediator.Send(message));
    }
}
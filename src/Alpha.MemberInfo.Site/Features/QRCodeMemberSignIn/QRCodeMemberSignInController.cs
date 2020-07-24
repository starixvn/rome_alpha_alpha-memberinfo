namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.Captcha.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.Web;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class QRCodeMemberSignInController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string IndexPath = "~/Features/QRCodeMemberSignIn/Views/Index.cshtml";
        private const string ShowQRCodePath = "~/Features/QRCodeMemberSignIn/Views/ShowQRCode.cshtml";
        private const string NotAuthorizedPath = "~/Features/QRCodeMemberSignIn/Views/_NotAuthorized.cshtml";
        private const string Separator = "; ";

        private readonly IMediator mediator;
        private readonly int ErrorCodeDefault = 998;

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

        [HttpPost, CaptchaValidation, AntiXsrfFilter]
        public async Task<ActionResult> CreateAccount(CreateAccountByQRCodeRequest message)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultCreateMemberQRCode
                {
                    ErrCode = ErrorCodeDefault,
                    ErrMsg = string.Join(Separator, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                });
            }

            if (message != null && !message.IsValid())
            {
                return Json(new ResultCreateMemberQRCode
                {
                    ErrCode = ErrorCodeDefault,
                    ErrMsg = message.IsInternal ? Lang.NotAuthorized : Resources.UsernameIsInvalid
                });
            }

            var result = await mediator.SendAsync(message).ConfigureAwait(false);

            if (result.Success)
            {
                return Json(new
                {
                    result.ErrCode,
                    result.ErrMsg,
                    result.CustIdCopyFrom,
                    result.RawPassword,
                    SearchAccountId = result.CustId,
                    SearchAccountName = message?.Username,
                    message?.FirstName,
                    message?.Credit,
                    message?.CreditMin,
                    message?.CreditMax,
                    message?.ShowPageTitle,
                    CreateMode = true
                });
            }
            else
            {
                ModelState.AddModelError("DBErrors", result.ErrMsg);
                return Json(result);
            }  
        }

        [HttpPost, AntiXsrfFilter]
        public ActionResult GetMemberCreditAndStatus(GetMemberCreditAndStatusRequest message)
            => Json(mediator.Send(message));

        [HttpGet]
        public ActionResult ShowQRCode(ShowQRCodeRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(message.IsInternal ? NotAuthorizedPath : InvalidPageViewPath);
            }

            var viewModel = mediator.Send(message);
            return View(viewModel == null ? InvalidPageViewPath : ShowQRCodePath, viewModel);
        }

        [HttpPost, AntiXsrfFilter]
        public ActionResult GenerateQrCode(GenerateQRCodeRequest message)
            => Json(mediator.Send(message));

        [HttpPost, AntiXsrfFilter]
        public ActionResult CheckExistedCustomer(CheckExistedCustomerRequest message)
           => Json(mediator.Send(message));
    }
}
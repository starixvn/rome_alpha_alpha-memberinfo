namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Captcha.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.Web.Filters;
    using Fanex.Logging.Extensions.Web;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class QRCodeMemberSignInController : SiteBaseController
    {
        private const string IndexPath = "~/Features/QRCodeMemberSignIn/Views/Index.cshtml";
        private const string ShowQRCodePath = "~/Features/QRCodeMemberSignIn/Views/ShowQRCode.cshtml";
        private const string NotAuthorizedPath = "~/Features/QRCodeMemberSignIn/Views/_NotAuthorized.cshtml";
        private const string Separator = "; ";
        private readonly string CaptchaInputName = "c_v";
        private readonly string CaptchaFunctionName = "c_f";
        private readonly int EmptyCaptchaErrorCode = 1;

        private readonly IUserSession userSession;
        private readonly int ErrorCodeDefault = 998;

        public QRCodeMemberSignInController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [HttpGet]
        public ActionResult Index(QRCodeMemberSignInRequest message)
            => View(message, IndexPath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> CreateAccount(CreateAccountByQRCodeRequest message)
        {
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
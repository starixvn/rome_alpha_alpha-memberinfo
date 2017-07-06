using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests.Transfer;
using MediatR;
using Sunplus.Agent.Model.Entities;

namespace Alpha.MemberInfo.Site.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class TransferController : SiteBaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly IUserSession userSession;

        public TransferController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [HttpGet]
        public async Task<ActionResult> CashBalance(CashBalanceTransferQuery message)
        {
            if (message == null || !message.IsValid() || !userSession.IsAllDayBalTransfer)
            {
                return View(InvalidPageViewPath);
            }

            return View(await mediator.SendAsync(message));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CashBalance(CashBalanceTransferCommand command)
        {
            Result result;

            if (command == null || !command.IsValid() || !userSession.IsAllDayBalTransfer)
            {
                result = Result.CreateErrorResult(AppResources.Transfer.BadRequest, 400);
            }
            else if (userSession.IsInternal)
            {
                result = Result.CreateErrorResult(AppResources.Transfer.NotAuthorized, 403);
            }
            else
            {
                result = await mediator.SendAsync(command);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<ActionResult> Hint(HintTransferQuery message)
        {
            if (message == null || !message.IsValid() || !userSession.IsAllDayBalTransfer)
            {
                return View(InvalidPageViewPath);
            }

            var viewModel = await mediator.SendAsync(message);

            return View(GetHintViewNameByLanguage(message.Language), viewModel);
        }

        [NonAction]
        private string GetHintViewNameByLanguage(string language)
        {
            switch (language)
            {
                case "zh-TW":
                case "zh-CN":
                case "th-TH":
                case "vi-VN":
                    return $"{nameof(Hint)}.{language}";

                default:
                    return nameof(Hint);
            }
        }
    }
}
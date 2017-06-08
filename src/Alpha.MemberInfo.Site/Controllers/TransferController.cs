using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using Alpha.Auth.Configurations;
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
            if (message == null || !message.IsValid() || !userSession.IsInternal)
            {
                return View(InvalidPageViewPath);
            }

            return View(await mediator.SendAsync(message));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CashBalance(CashBalanceTransferCommand command)
        {
            Result result;

            if (command == null || !command.IsValid() || !userSession.IsInternal)
            {
                result = Result.CreateErrorResult("Bad Request.", 400);
            }
            else
            {
                command.UplineId = userSession.CustId;
                command.SuperIdsTransferTotal = AppSettings.GetConfigValue("");
                command.SubAccId = userSession.SubAccId;
                command.IP = userSession.UserLoginIp;
                command.LanguageKey = userSession.LangKey;

                result = await mediator.SendAsync(command);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}
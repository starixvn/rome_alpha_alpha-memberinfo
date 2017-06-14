using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using Alpha.MemberInfo.Enumerations;
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
                result = Result.CreateErrorResult("Bad Request.", 400);
            }
            else if (userSession.IsInternal)
            {
                result = Result.CreateErrorResult(AppResources.Transfer.NotAuthorized, 403);
            }
            else
            {
                command.UplineId = userSession.CustId;
                command.SuperIdsTransferTotal = Sunplus.Agent.Model.BL.Definitions.AppSettings.AllowedListSuperAccounts;
                command.SubAccId = userSession.SubAccId;
                command.IP = userSession.UserLoginIp ?? "127.0.0.1";
                command.LanguageKey = userSession.LangKey;
                command.IsAllDayBalTransfer = userSession.IsAllDayBalTransfer;
                command.CurrentUserLevel = Enumeration.FromValue<UserLevel>(userSession.RoleId);

                result = await mediator.SendAsync(command);
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}
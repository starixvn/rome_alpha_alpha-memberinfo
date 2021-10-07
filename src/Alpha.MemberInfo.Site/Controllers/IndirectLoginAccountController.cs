namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Site.Requests.IndirectLoginAccount;
    using Alpha.Web.Filters;
    using MediatR;

    public class IndirectLoginAccountController : SiteBaseController
    {
        public IndirectLoginAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ManageAccountQuery message)
            => await ViewAsync(message);

        public ActionResult NewAccount(NewAccountQuery message)
        {
            AssignDarkModeViewData();
            return View(mediator.Send(message));

        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> NewAccount(NewAccountCommand message)
            => await JsonAsync(message, JsonRequestBehavior.DenyGet);

        public async Task<ActionResult> ValidateAccount(ValidateAccountQuery message)
            => await JsonAsync(message);

        public ActionResult UpdatePassword(UpdatePasswordQuery message)
            => View(message, "~/Views/IndirectLoginAccount/UpdatePassword.cshtml");

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand message)
            => await JsonAsync(message, JsonRequestBehavior.DenyGet);

        public ActionResult DisconnectPopup(DisconnectCommand message)
        {
            AssignDarkModeViewData();
            return View(message);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Disconnect(DisconnectCommand message)
            => await JsonAsync(message, JsonRequestBehavior.DenyGet);

        public async Task<ActionResult> Redirect(RedirectQuery message)
        {
            if (message.IsValid())
            {
                var url = await mediator.SendAsync(message);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    return Redirect(url);
                }
            }

            return new EmptyResult();
        }
    }
}
namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using MediatR;
    using Requests.CreditBalance;
    using ViewModels.CreditBalance;

    public class CreditBalanceController : SiteBaseController
    {
        public CreditBalanceController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(CreditBalanceQuery message)
        {
            AssignDarkModeViewData();
            return View(message.IsValid()
                ? await mediator.SendAsync(message)
                : new CreditBalanceViewModel(message));
        }
    }
}
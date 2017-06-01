using System.Web.Mvc;
using System.Web.SessionState;
using Alpha.MemberInfo.Site.Requests.Transfer;
using MediatR;

namespace Alpha.MemberInfo.Site.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class CashBalanceTransferController : SiteBaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";

        public CashBalanceTransferController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult Index(CashBalanceTransferQuery message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(mediator.Send(message));
        }

        //[HttpPost]
        //public ActionResult Transfer(CashBalanceTransferQuery message)
        //{
        //    if (message != null && !message.IsValid())
        //    {
        //        return View(InvalidPageViewPath);
        //    }

        //    return View(mediator.Send(message));
        //}
    }
}
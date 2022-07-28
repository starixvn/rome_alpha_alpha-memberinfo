namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ReachedThresholdController : SiteBaseController
    {
        private const string IndexPath = "~/Features/MyMonitorAccounts/ReachedThreshold/Views/Index.cshtml";
        private const string ReachHistoryPath = "~/Features/MyMonitorAccounts/ReachedThreshold/Views/ReachHistory.cshtml";

        public ReachedThresholdController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ReachedThresholdQuery message)
            => await ViewAsync(message, IndexPath);

        public async Task<ActionResult> GetReachHistory(ReachHistoryQuery message)
            => await ViewAsync(message, ReachHistoryPath);

        public async Task<ActionResult> GetReachlistThreshold(ReachlistAccountsQuery message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Excel(ReachedThresholdExcelQuery message)
            => await ExcelViewAsync(message);
    }
}
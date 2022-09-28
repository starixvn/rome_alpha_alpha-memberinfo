namespace Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ReachedThresholdController : SiteBaseController
    {
        private const string IndexPath = "~/Features/MyWatchlist/ReachedThreshold/Views/Index.cshtml";
        private const string ReachHistoryPath = "~/Features/MyWatchlist/ReachedThreshold/Views/ReachHistory.cshtml";

        public ReachedThresholdController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ReachedThresholdQuery message)
            => await ViewAsync(message, IndexPath);

        public async Task<ActionResult> ReachHistoryIndex(ReachHistoryQuery message)
            => await ViewAsync(message, ReachHistoryPath);

        [HttpPost]
        public async Task<ActionResult> GetReachlistThreshold(ReachlistAccountsQuery message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetReachHistoryList(ReachHistoryListQuery message)
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
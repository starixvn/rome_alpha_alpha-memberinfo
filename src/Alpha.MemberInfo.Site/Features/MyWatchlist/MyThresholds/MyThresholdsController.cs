namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.ViewModels;
    using Alpha.Web.Filters;
    using MediatR;

    public class MyThresholdsController : SiteBaseController
    {
        private const string IndexPath = "~/Features/MyWatchlist/MyThresholds/Views/Index.cshtml";
        private const string SettingsPath = "~/Features/MyWatchlist/MyThresholds/Views/Settings.cshtml";

        public MyThresholdsController(IMediator mediator)
            : base(mediator)
        {
        }

        public Task<ActionResult> Index(MyThresholdIndexPageQuery message)
            => ViewAsync<MyThresholdIndexPageViewModel>(message, IndexPath);

        [HttpPost]
        public Task<ActionResult> GetHistory(MyThresholdHistoryQuery message)
            => JsonAsync(message);

        public Task<ActionResult> Settings(MyThresholdQuery message)
            => ViewAsync(message, SettingsPath);

        [HttpPost, AntiXsrfFilter]
        public Task<ActionResult> UpdateSettings(MyThresholdCommand message)
            => JsonAsync(message, JsonRequestBehavior.DenyGet);
    }
}
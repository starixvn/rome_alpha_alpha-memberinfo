namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MemberBettingBehaviorController : SiteBaseController
    {

        private const string IndexPath = "~/Features/MemberBettingBehavior/Views/Index.cshtml";

        public MemberBettingBehaviorController(IMediator mediator) : base(mediator) 
        {
        }

        public ActionResult Index()
        {
            var viewModel = new MemberBettingBehaviorViewModel();
            return View(IndexPath, viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> MemberProfile(MemberProfileQuery message)
            => await JsonAsync(message);

        [HttpGet]
        public async Task<ActionResult> MemberBettingStatistic(MemberBettingStatisticQuery message)
            => await JsonAsync(message);

        [HttpGet]
        public async Task<ActionResult> Excel(MemberBettingBehaviorExcelQuery message)
            => await ExcelViewAsync(message);
    }
}
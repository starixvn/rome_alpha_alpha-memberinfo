namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels;
    using MediatR;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MemberBettingBehaviorController : SiteBaseController
    {

        private const string IndexPath = "~/Features/MemberBettingBehavior/Views/Index.cshtml";
        private readonly IUser viewer;
        private readonly CheckBelongingService checkBelongingService;

        public MemberBettingBehaviorController(IMediator mediator, CheckBelongingService checkBelongingService, IUser viewer) : base(mediator) 
        {
            this.checkBelongingService = checkBelongingService;
            this.viewer = viewer;
        }

        public ActionResult Index()
        {
            var isDarkMode = !string.IsNullOrWhiteSpace(viewer.DarkModeName);
            var viewModel = new MemberBettingBehaviorViewModel(isDarkMode, viewer.Language);
            return View(IndexPath, viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> MemberProfile(MemberProfileQuery message)
        {
            var memberCustId = await GetMemberDownlineCustId(message.ViewerId, message.SubAccId, message.UserName);
            if (memberCustId > 0)
            {
                message.MemberCustId = memberCustId;
                return await JsonAsync(message);
            }
            return Content(JsonConvert.SerializeObject(Result.CreateErrorResult("")));
        }

        [HttpGet]
        public async Task<ActionResult> MemberBettingStatistic(MemberBettingStatisticQuery message)
        {
            var memberCustId = await GetMemberDownlineCustId(message.ViewerId, message.SubAccId, message.UserName);
            if (memberCustId > 0)
            {
                message.MemberCustId = memberCustId;
                return await JsonAsync(message);
            }

            return Content(JsonConvert.SerializeObject(Result.CreateErrorResult("")));
        }

        [HttpGet]
        public async Task<ActionResult> Excel(MemberBettingBehaviorExcelQuery message)
        {
            var memberCustId = await GetMemberDownlineCustId(message.ViewerId, message.SubAccId, message.UserName);
            if (memberCustId > 0)
            {
                message.MemberCustId = memberCustId;
                return await ExcelViewAsync(message);
            }
            return Content(JsonConvert.SerializeObject(Result.CreateErrorResult("")));
        }

        private async Task<int> GetMemberDownlineCustId(int custId, int subAccId, string username)
        {
            var downlineInfo = await checkBelongingService.CheckBelongingByUsername(custId, subAccId, username);
            if (downlineInfo.CustId > 0 && downlineInfo.RoleId == UserLevelConstant.Member)
            {
                return downlineInfo.CustId;
            }

            return 0;
        }
    }
}
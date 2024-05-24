namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests;
    using Alpha.MemberInfo.Site.Features.MemberBettingBehavior.ViewModels;
    using Fanex.Customer.Client.Services;
    using MediatR;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MemberBettingBehaviorController : SiteBaseController
    {

        private const string IndexPath = "~/Features/MemberBettingBehavior/Views/Index.cshtml";
        private readonly ICustomerService customerService;

        public MemberBettingBehaviorController(IMediator mediator, ICustomerService customerService) : base(mediator) 
        {
            this.customerService = customerService;
        }

        public ActionResult Index()
        {
            var viewModel = new MemberBettingBehaviorViewModel();
            return View(IndexPath, viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> MemberProfile(MemberProfileQuery message)
        {
            var memberCustId = await GetMemberCustId(message.CustId, message.SubAccId, message.UserName);
            if(memberCustId > 0)
            {
                message.MemberCustId = memberCustId;
                return await JsonAsync(message);
            }
            return Content(JsonConvert.SerializeObject(Result.CreateErrorResult("")));
        }

        [HttpGet]
        public async Task<ActionResult> MemberBettingStatistic(MemberBettingStatisticQuery message)
        {
            var memberCustId = await GetMemberCustId(message.CustId, message.SubAccId, message.UserName);
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
            var memberCustId = await GetMemberCustId(message.CustId, message.SubAccId, message.UserName);
            if (memberCustId > 0)
            {
                message.MemberCustId = memberCustId;
                return await ExcelViewAsync(message);
            }
            return Content(JsonConvert.SerializeObject(Result.CreateErrorResult("")));
        }

        private async Task<int> GetMemberCustId(int custId, int subAccId, string username)
        {
            var customer = await customerService.GetDownlineByUsername(custId, subAccId, username);
            if (customer.CustId > 0 && customer.RoleId == 1)
            {
                return customer.CustId;
            }

            return 0;
        }
    }
}
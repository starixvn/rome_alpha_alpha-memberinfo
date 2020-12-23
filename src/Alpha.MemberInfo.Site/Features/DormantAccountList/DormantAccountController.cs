namespace Alpha.MemberInfo.Site.Features.DormantAccountList
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.DormantAccountList.Requests;
    using MediatR;

    public class DormantAccountController : SiteBaseController
    {
        public DormantAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(DormantAccountQuery message)
            => await ViewAsync(message, "~/Features/DormantAccountList/Views/Index.cshtml");

        [HttpGet]
        public async Task<ActionResult> Excel(DormantAccountExcelQuery message)
            => await ExcelViewAsync(message);
    }
}
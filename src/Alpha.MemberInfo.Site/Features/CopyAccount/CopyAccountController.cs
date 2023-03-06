using Alpha.MemberInfo.Site.Controllers;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.Web.Filters;
using MediatR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyAccountController : SiteBaseController
    {
        private const string IndexPath = "~/Features/CopyAccount/Views/Index.cshtml";

        public CopyAccountController(IMediator mediator) 
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(CopyAccountQuery query)
            => await ViewAsync(query, IndexPath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Index(CopyAccountCommand command)
            => await JsonAsync(command);

        public async Task<ActionResult> GetCredit(CreditQuery query)
            => await JsonAsync(query);

        public async Task<ActionResult> CheckUsername(CheckUsernameQuery query)
            => await JsonAsync(query);
    }
}
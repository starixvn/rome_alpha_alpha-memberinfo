using Alpha.MemberInfo.Site.Controllers;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
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
    }
}
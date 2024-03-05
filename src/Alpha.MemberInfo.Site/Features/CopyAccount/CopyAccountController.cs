using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Controllers;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using Alpha.Web.Filters;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.Site.Features.CopyAccount.ViewModels;

namespace Alpha.MemberInfo.Site.Features.CopyAccount
{
    public class CopyAccountController : SiteBaseController
    {
        private const string IndexPath = "~/Features/CopyAccount/Views/Index.cshtml";
        private const string ResultPath = "~/Features/CopyAccount/Views/_CopyResult.cshtml";

        public CopyAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(CopyAccountQuery query)
            => await ViewAsync(query, IndexPath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Index(CopyAccountCommand command)
        {
            if (!command.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized, BadRequestErrorCode), JsonRequestBehavior.AllowGet);
            }

            var result = await mediator.SendAsync(command);
            if (result.Usernames.Any())
            {
                return PartialView(ResultPath, new CopyAccountResultViewModel
                {
                    Result = result,
                    Credit = command.Credit,
                    NumberOfAccounts = command.NumberOfAccounts,
                    NoOfFailedAccount = command.NumberOfAccounts - result.Usernames.Count()
                });
            }
            else
            {
                return Json(result);
            }
        }

        public async Task<ActionResult> GetCredit(CreditQuery query)
            => await JsonAsync(query);

        public async Task<ActionResult> CheckUsername(CheckUsernameQuery query)
            => await JsonAsync(query);

        public async Task<ActionResult> CheckExistedCustomer(CheckExistedCustomerQuery query)
            => await JsonAsync(query);
    }
}
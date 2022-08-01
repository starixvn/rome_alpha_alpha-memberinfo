namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MyWatchlistAccountsController : SiteBaseController
    {
        private const string IndexPath = "~/Features/MyWatchlist/MyWatchlistAccounts/Views/Index.cshtml";
        private const string ManageWatchlistAccountsPath = "~/Features/MyWatchlist/MyWatchlistAccounts/Views/ManageWatchlistAccounts.cshtml";
        private const string ManageGlobalThresholdPath = "~/Features/MyWatchlist/MyWatchlistAccounts/Views/ManageGlobalThreshold.cshtml";
        private const string ManagePersonalThresholdPresetPath = "~/Features/MyWatchlist/MyWatchlistAccounts/Views/ManagePersonalThresholdPreset.cshtml";

        public MyWatchlistAccountsController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(MyWatchlistAccountsQuery message)
            => await ViewAsync(message, IndexPath);

        public async Task<ActionResult> ManageWatchlistAccounts(ManageWatchlistAccountsQuery message)
         => await ViewAsync(message, ManageWatchlistAccountsPath);

        public async Task<ActionResult> ManageGlobalThreshold(ManageGlobalThresholdQuery message)
            => await ViewAsync(message, ManageGlobalThresholdPath);

        public async Task<ActionResult> ManagePersonalThresholdPreset(ManagePersonalThresholdPresetQuery message)
            => await ViewAsync(message, ManagePersonalThresholdPresetPath);

        [HttpPost]
        public async Task<ActionResult> GetWatchlistAccounts(WatchlistAccountsQuery message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> AddPersonalThreshold(AddPersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateMultiplePersonalThreshold(UpdateMultiplePersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> DeleteMultiplePersonalThreshold(DeleteMultiplePersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateGlobalThreshold(UpdateGlobalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetPersonalPresets(PersonalPresetQuery message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> AddMultiplePersonalPreset(AddMultiplePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdatePersonalPreset(UpdatePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> DeletePersonalPreset(DeletePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(Result.CreateErrorResult(Lang.NotAuthorized), JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }
    }
}
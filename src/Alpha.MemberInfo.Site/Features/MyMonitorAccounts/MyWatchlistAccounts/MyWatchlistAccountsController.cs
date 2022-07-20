namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts
{
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MyWatchlistAccountsController : SiteBaseController
    {
        private const string IndexPath = "~/Features/MyMonitorAccounts/MyWatchlistAccounts/Views/Index.cshtml";
        private const string ManageWatchlistAccountsPath = "~/Features/MyMonitorAccounts/MyWatchlistAccounts/Views/ManageWatchlistAccounts.cshtml";
        private const string ManageGlobalThresholdPath = "~/Features/MyMonitorAccounts/MyWatchlistAccounts/Views/ManageGlobalThreshold.cshtml";
        private const string ManagePersonalThresholdPresetPath = "~/Features/MyMonitorAccounts/MyWatchlistAccounts/Views/ManagePersonalThresholdPreset.cshtml";

        private readonly Result ErrorDefault = Result.CreateErrorResult(Lang.NotAuthorized);

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
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddPersonalThreshold(AddPersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMultiplePersonalThreshold(UpdateMultiplePersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMultiplePersonalThreshold(DeleteMultiplePersonalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateGlobalThreshold(UpdateGlobalThresholdCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetPersonalPresets(PersonalPresetQuery message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddMultiplePersonalPreset(AddMultiplePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePersonalPreset(UpdatePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePersonalPreset(DeletePersonalPresetCommand message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(await mediator.SendAsync(message), JsonRequestBehavior.AllowGet);
        }
    }
}
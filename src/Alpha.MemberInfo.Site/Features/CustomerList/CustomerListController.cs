namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using System.Web.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services.Transfer;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web.Filters;
    using MediatR;
    using AlphaPlus.User.Models;

    public class CustomerListController : SiteBaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";
        private const string MultipleDoubleCommPath = "~/Features/CustomerList/Views/MultipeDoubleComm.cshtml";
        private readonly Result ErrorDefault = Result.CreateErrorResult(Lang.NotAuthorized);
        private readonly TransferService transferService;
        private readonly IUser viewer;

        public CustomerListController(
            IMediator mediator,
            TransferService transferService,
            IUser viewer)
            : base(mediator)
        {
            this.transferService = transferService;
            this.viewer = viewer;
        }

        [Route("CustomerList/master")]
        [Route("CustomerList/agent")]
        [Route("CustomerList/member")]
        [HttpGet]
        public ActionResult Index(CustomerListRequest message)
        {
            message.SupportAutoTransfer = transferService.IsEnableAutoTransfer(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            message.AutoTransferAffect = transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            return View(message, IndexPath);
        }

        [HttpPost]
        public ActionResult GetCustomerListData(FetchDownlinesListRequest message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            if (message.EnableAllDayBalanceTransfer.HasValue && transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies")))
            {
                if (!message.EnableAllDayBalanceTransfer.Value && transferService.IsAllDayBalanceTransfer(message.CustId))
                {
                    return Json(Result.CreateErrorResult(Resources.ADBTOnError, 100), JsonRequestBehavior.AllowGet);
                }

                if (message.EnableAllDayBalanceTransfer.Value && !transferService.IsAllDayBalanceTransfer(message.CustId))
                {
                    return Json(Result.CreateErrorResult(Resources.ADBTOffError, 100), JsonRequestBehavior.AllowGet);
                }
            }

            message.SupportAutoTransfer = transferService.IsEnableAutoTransfer(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            message.AutoTransferAffect = transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerAllStatus(CustomerStatusQuery message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            message.SupportAutoTransfer = transferService.IsEnableAutoTransfer(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            message.AutoTransferAffect = transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MultipleEditPopup(CustomerStatusMultipleQuery message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            message.SupportAutoTransfer = transferService.IsEnableAutoTransfer(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            message.AutoTransferAffect = transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCustomerStatus(CustomerNonProductStatusCommand command)
        {
            if (!command.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            command.SupportAutoTransfer = transferService.IsEnableAutoTransfer(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));
            command.AutoTransferAffect = transferService.IsAutoTransferAffect(viewer, Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies"));

            return Json(mediator.Send(command), JsonRequestBehavior.DenyGet);
        }

        public ActionResult MultipleDoubleComm(MultipleDoubleCommRequest message)
            => View(message, MultipleDoubleCommPath);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateMultipleDoubleComm(UpdateMultipleDoubleCommCommand command)
        {
            if (!command.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(command), JsonRequestBehavior.DenyGet);
        }
    }
}
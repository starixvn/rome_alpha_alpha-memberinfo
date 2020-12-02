namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Web.Mvc;

    public class CustomerListController : BaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";
        private const string MultipleDoubleCommPath = "~/Features/CustomerList/Views/MultipeDoubleComm.cshtml";
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private readonly Result ErrorDefault = Result.CreateErrorResult("Invalid Request.");
        private readonly IMediator mediator;

        public CustomerListController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("CustomerList/master")]
        [Route("CustomerList/agent")]
        [Route("CustomerList/member")]
        [HttpGet]
        public ActionResult Index(CustomerListRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(IndexPath, mediator.Send(message));
        }

        [HttpPost]
        public ActionResult GetCustomerListData(FetchDownlinesListRequest message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerAllStatus(CustomerStatusQuery message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MultipleEditPopup(CustomerStatusMultipleQuery message)
        {
            if (!message.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCustomerStatus(CustomerNonProductStatusCommand command)
        {
            if (!command.IsValid())
            {
                return Json(ErrorDefault, JsonRequestBehavior.AllowGet);
            }

            return Json(mediator.Send(command), JsonRequestBehavior.DenyGet);
        }

        public ActionResult MultipleDoubleComm(MultipleDoubleCommRequest message)
        {
            if (message != null && !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            return View(MultipleDoubleCommPath, null, mediator.Send(message));
        }


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
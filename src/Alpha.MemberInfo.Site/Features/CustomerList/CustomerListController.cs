namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using System.Web.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web.Filters;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class CustomerListController : SiteBaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";
        private const string MultipleDoubleCommPath = "~/Features/CustomerList/Views/MultipeDoubleComm.cshtml";
        private readonly Result ErrorDefault = Result.CreateErrorResult(Lang.NotAuthorized);
        private readonly IUserSession userSession;

        public CustomerListController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [Route("CustomerList/master")]
        [Route("CustomerList/agent")]
        [Route("CustomerList/member")]
        [HttpGet]
        public ActionResult Index(CustomerListRequest message)
            => View(message, IndexPath);

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
            message.SubPageAccess = userSession.SubPageAccess;

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
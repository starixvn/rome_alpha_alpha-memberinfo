namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Web.Mvc;

    public class CustomerListController : MemberInfoBaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";

        public CustomerListController(IMediator mediator) : base(mediator)
        {
        }
        [Route("CustomerList/master")]
        [Route("CustomerList/agent")]
        [Route("CustomerList/member")]
        [HttpGet]
        public ActionResult Index(CustomerListRequest message)
          => View(message, IndexPath);

        [HttpPost]
        public ActionResult GetCustomerListData(FetchDownlinesListRequest message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        public ActionResult GetCustomerAllStatus(CustomerStatusQuery message)
           => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public ActionResult MultipleEditPopup(CustomerStatusMultipleQuery message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCustomerStatus(CustomerNonProductStatusCommand command)
          => Json(mediator.Send(command), JsonRequestBehavior.DenyGet);
    }
}
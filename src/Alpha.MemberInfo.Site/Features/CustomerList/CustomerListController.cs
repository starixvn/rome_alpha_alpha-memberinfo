namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.Shared;
    using MediatR;
    using System.Web.Mvc;

    public class CustomerListController : MemberInfoBaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";
        private const string EditPopupViewPath = "~/Features/CustomerList/Views/EditPopup.cshtml";
        private const string EditPopupMultiplePath = "~/Features/CustomerList/Views/EditPopupMultiple.cshtml";
        private const string StatusPopupPath = "~/Features/CustomerList/Views/StatusPopup.cshtml";

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

        //public ActionResult EditPopup(CustomerStatusQuery message)
        //  => View(message, EditPopupViewPath);

        public ActionResult MultipleEditPopup(CustomerStatusMultipleQuery message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        //public ActionResult StatusPopup(CustomerStatusQuery message)
        // => View(message, StatusPopupPath);
    }
}
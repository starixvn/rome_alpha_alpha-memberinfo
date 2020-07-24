namespace Alpha.MemberInfo.Site.Features.CustomerList
{
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web;
    using MediatR;
    using System.Web.Mvc;

    public class CustomerListController : BaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";

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
           => View(IndexPath, mediator.Send(message));

        [HttpPost]
        public ActionResult GetCustomerListData(FetchDownlinesListRequest message)
          => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
    }
}
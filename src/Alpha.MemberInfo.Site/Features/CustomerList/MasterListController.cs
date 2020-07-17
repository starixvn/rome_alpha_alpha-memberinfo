namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web;
    using MediatR;
    using System.Web.Mvc;

    public class MasterListController : BaseController
    {
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";

        private readonly IMediator mediator;

        public MasterListController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("MasterList/master")]
        [Route("MasterList/agent")]
        [Route("MasterList/member")]
        [HttpGet]
        public ActionResult Index(MasterListRequest message)
            => View(IndexPath, mediator.Send(message));
    }
}
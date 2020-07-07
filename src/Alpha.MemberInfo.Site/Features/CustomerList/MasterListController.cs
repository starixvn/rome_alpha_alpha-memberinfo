namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.Web;
    using MediatR;
    using System.Web.Mvc;

    public class MasterListController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string IndexPath = "~/Features/CustomerList/Views/Index.cshtml";
      
        private readonly IMediator mediator;

        public MasterListController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult Index(MasterListRequest message)
        {
            return View(IndexPath, mediator.Send(message));
        }
    }
}
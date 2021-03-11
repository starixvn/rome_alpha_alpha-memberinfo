namespace Alpha.MemberInfo.Site.Features.Shared
{
    using Alpha.MemberInfo.Site.Features.Shared.Requests;
    using Alpha.Web;
    using MediatR;
    using System.Web.Mvc;

    public class UserNameSuggestionController : BaseController
    {
        private readonly IMediator mediator;

        public UserNameSuggestionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
            => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);
    }
}
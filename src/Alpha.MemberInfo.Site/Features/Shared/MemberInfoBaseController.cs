namespace Alpha.MemberInfo.Site.Features.Shared
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.Shared.Requests;
    using MediatR;
    using System.Web.Mvc;
    using Web;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods

    public abstract class MemberInfoBaseController : BaseAsyncController
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
    {
        private const string NoInformationPageViewPath = "~/Features/Shared/Views/NoInformation.cshtml";

        private readonly Result ErrorDefault = Result.CreateErrorResult("Invalid Request.");

        protected readonly IMediator mediator;

        protected MemberInfoBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message, string viewPath)
             => (message as BaseRequest).IsValid() ?
                View(viewPath, mediator.Send(message)) : View(NoInformationPageViewPath);

        protected ActionResult JsonData<TResponse>(
           IRequest<TResponse> message,
           JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            if (!(message as BaseRequest).IsValid())
            {
                return Json(ErrorDefault, jsonRequestBehavior);
            }

            return Json(mediator.Send(message), jsonRequestBehavior);
        }
    }
}
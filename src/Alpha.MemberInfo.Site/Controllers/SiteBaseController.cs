namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using Web;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public abstract class SiteBaseController : BaseController
    {
        protected const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string InvalidExcelFileName = "Invalid";
        private const int BadRequestErrorCode = 999;

        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message, string viewName)
            => (message as MemberInfoBaseQuery).IsValid()
                ? View(viewName, mediator.Send(message))
                : View(InvalidPageViewPath);

        protected FileContentResult ExcelView(IRequest<ExcelFileViewModel> message)
            => (message as MemberInfoBaseQuery).IsValid()
                ? GetExcelContent(mediator.Send(message))
                : GetExcelContent(new ExcelFileViewModel(InvalidExcelFileName, new byte[] { }));

        protected ActionResult Json<TResponse>(IRequest<TResponse> message)
            => (message as MemberInfoBaseQuery).IsValid() ? 
                Json(mediator.Send(message), JsonRequestBehavior.AllowGet) :
                Json(Result.CreateErrorResult(Lang.NotAuthorized, BadRequestErrorCode), JsonRequestBehavior.AllowGet);

        private FileContentResult GetExcelContent(ExcelFileViewModel viewModel)
            => File(viewModel.Content, ExcelFileViewModel.ContentType, viewModel.Name);

        protected async Task<ActionResult> ViewAsync<TResponse>(IAsyncRequest<TResponse> message)
            => (message as MemberInfoBaseQuery).IsValid()
                ? View(await mediator.SendAsync(message))
                : View(InvalidPageViewPath);

        protected async Task<ActionResult> ViewAsync<TResponse>(IAsyncRequest<TResponse> message, string viewName)
            => (message as MemberInfoBaseQuery).IsValid()
                ? View(viewName, await mediator.SendAsync(message))
                : View(InvalidPageViewPath);

        protected async Task<ActionResult> JsonAsync<TResponse>(
            IAsyncRequest<TResponse> message,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
            => (message as MemberInfoBaseQuery).IsValid() ?
                Json(await mediator.SendAsync(message), jsonRequestBehavior) :
                Json(Result.CreateErrorResult(Lang.NotAuthorized, BadRequestErrorCode), jsonRequestBehavior);

        protected async Task<FileContentResult> ExcelViewAsync(IAsyncRequest<ExcelFileViewModel> message)
            => (message as MemberInfoBaseQuery).IsValid()
                ? GetExcelContent(await mediator.SendAsync(message))
                : GetExcelContent(new ExcelFileViewModel(InvalidExcelFileName, new byte[] { }));
    }
}
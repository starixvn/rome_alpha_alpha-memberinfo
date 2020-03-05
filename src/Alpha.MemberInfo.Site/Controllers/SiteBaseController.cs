namespace Alpha.MemberInfo.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Site.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using Web;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public abstract class SiteBaseController : BaseController
    {
        private const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";
        private const string InvalidExcelFileName = "Invalid";

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
            => Json(mediator.Send(message), JsonRequestBehavior.AllowGet);

        private FileContentResult GetExcelContent(ExcelFileViewModel viewModel)
            => File(viewModel.Content, ExcelFileViewModel.ContentType, viewModel.Name);
    }
}
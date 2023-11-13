using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CreateAccount.Requests;
using Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels;
using Alpha.Web;
using Alpha.Web.Filters;
using Fanex.Customer.Client.Configurations;
using Fanex.Customer.Client.Params;
using Fanex.Customer.Client.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alpha.MemberInfo.Site.Features.CreateAccount
{
    public class PTTemplateController : BaseController
    {
        private const string PTTemplatePath = "~/Features/CreateAccount/Views/PTTemplate.cshtml";

        private readonly IUser viewer;
        private readonly ICustomerService customerService;
        private readonly CustomerClientConfiguration customerClientConfiguration;

        public PTTemplateController(IUser viewer, ICustomerService customerService, CustomerClientConfiguration customerClientConfiguration)
        {
            this.viewer = viewer;
            this.customerService = customerService;
            this.customerClientConfiguration = customerClientConfiguration;
        }

        public ActionResult Index()
            => View(PTTemplatePath, new PTTemplateViewModel(viewer.LevelId));

        [HttpGet]
        public async Task<ActionResult> GetPTSettings()
            => Content(await customerService.GetPTSettings(viewer.Id, viewer.LevelId, viewer.Site, viewer.CurrencyId, customerClientConfiguration.SystemId, viewer.Language));

        [HttpGet]
        public async Task<ActionResult> GetPTTemplates()
            => Content(await customerService.GetPTTemplates(viewer.Id));

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Create(CreatePTTemplateRequest request)
        {
            if (viewer.IsInternal)
            {
                return UnAuthorized();
            }

            return Content(await customerService.CreatePTTemplate(new CreatePTTemplateParam(
                viewer.Id,
                request.Name,
                request.IsDefault,
                request.Settings,
                viewer.SubAccId,
                viewer.Language,
                viewer.IP)));
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Edit(EditPTTemplateRequest request)
        {
            if (viewer.IsInternal)
            {
                return UnAuthorized();
            }

            return Content(await customerService.EditPTTemplate(new EditPTTemplateParam(
                viewer.Id,
                request.Id,
                request.IsDefault,
                request.Settings,
                viewer.SubAccId,
                viewer.Language,
                viewer.IP)));
        }

        [HttpPost, AntiXsrfFilter]
        [Route("PTTemplate/Delete/{templateId}")]
        public async Task<ActionResult> Delete(int templateId)
        {
            if (viewer.IsInternal)
            {
                return UnAuthorized();
            }

            return Content(await customerService.DeletePTTemplate(new DeletePTTemplateParam(templateId, viewer.Id, viewer.SubAccId, viewer.Language, viewer.IP)));
        }

        private ActionResult UnAuthorized()
            => Content(JsonConvert.SerializeObject(Result.CreateErrorResult(Lang.NotAuthorized), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}
using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CreateAccount.Requests;
using Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels;
using Alpha.Web;
using Alpha.Web.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Sunplus.Agent.Utilities;

namespace Alpha.MemberInfo.Site.Features.CreateAccount
{
    public class CreateAccountController : BaseController
    {
        private const string IndexPath = "~/Features/CreateAccount/Views/Index.cshtml";

        private readonly IUser viewer;
        private readonly Encryptor encryptor;
        private readonly ICreateAccountService createAccountService;

        public CreateAccountController(IUser viewer, Encryptor encryptor, ICreateAccountService createAccountService)
        {
            this.viewer = viewer;
            this.encryptor = encryptor;
            this.createAccountService = createAccountService;
        }

        public async Task<ActionResult> Index()
        {
            var limitNewDownlineResult = await createAccountService.CheckAllLimitedRules(
                   viewer.Id,
                   viewer.LevelId,
                   viewer.Name,
                   viewer.Language);

            return View(IndexPath, new CreateAccountViewModel(limitNewDownlineResult.IsFailed, limitNewDownlineResult.Message, viewer.LevelId));
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Index(CreateAccountRequest request)
        {
            if (viewer.IsInternal)
            {
                return UnAuthorized();
            }

            var generalInfo = request.GeneralInfo;
            generalInfo.Password = encryptor.CfsEnCode(generalInfo.Password);
            generalInfo.UpLineRoleId = viewer.LevelId;

            return Content(await createAccountService.CreateAccount(
                viewer.Id,
                viewer.SubAccId,
                viewer.IP,
                viewer.Language,
                request.UIBehavior,
                generalInfo,
                request.ProductGroups));
        }

        public async Task<ActionResult> GetCustomerInfo()
            => Content(await createAccountService.GetCustomerInfo(viewer.Id, viewer.LevelId, viewer.Language));

        private ActionResult UnAuthorized()
           => Content(JsonConvert.SerializeObject(Result.CreateErrorResult(Lang.NotAuthorized), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}
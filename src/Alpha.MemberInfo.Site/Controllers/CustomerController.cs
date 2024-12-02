using System;
using System.Configuration;
using System.Net;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Configurations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels;
using Alpha.MemberInfo.Site.Requests.Customer;
using Alpha.MemberInfo.Site.ViewModels.Customer;
using Alpha.Web;
using Alpha.Web.Filters;
using Alpha.Web.Helpers;
using Fanex.Cryptography.Aes;
using Fanex.Customer.Client.Configurations;
using Fanex.Customer.Client.Constants;
using Fanex.Customer.Client.Params;
using Fanex.Customer.Client.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sunplus.Agent.Utilities;

namespace Alpha.MemberInfo.Site.Controllers
{
    public class CustomerController : BaseController
    {
        private const string DCSStaticSessionKey = "DCSStaicAppPathSessionKey";
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Controllers.CustomerController.PasswordHash";
        private const string AssetsVersionCacheKey = "Customer:AssetsVersion";
        private readonly int systemId;
        private readonly Encryptor encryptor;
        private readonly SessionManager sessionManager;
        private readonly IAesService aesService;
        private readonly ICustomerService customerService;

        public CustomerController(
            Encryptor encryptor,
            SessionManager sessionManager,
            IAesService aesService,
            CustomerClientConfiguration clientConfiguration,
            ICustomerService customerService)
        {
            this.encryptor = encryptor;
            this.sessionManager = sessionManager;
            this.aesService = aesService;
            systemId = clientConfiguration.SystemId;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.customerService = customerService;
        }

        private string CDNUri => sessionManager.Get<string>(DCSStaticSessionKey) ?? ConfigurationManager.AppSettings["Fanex.Customer:CdnUri"];

        public async Task<ActionResult> CreateDirectMember(CreateCustomerQuery query)
        {
            var limitedRulesResult = await customerService.CheckAllLimitedRules(query.ViewerId, query.ViewerLevelId, query.ActorUsername, query.Language);

            var viewModel = limitedRulesResult.IsFailed
                ? new CreateDirectMemberViewModel(query, true, limitedRulesResult.Message)
                : new CreateDirectMemberViewModel(
                query,
                new AppConfiguration(GetApplicationPath(), systemId),
                CDNUri,
                GetAssetsVersion(),
                GetHashKey(),
                await customerService.GetSuggestDirectMemberUsername(query.CustName));

            return View("~/Views/Customer/CreateDirectMember.cshtml", viewModel);
        }

        public async Task<ActionResult> Create(CreateCustomerQuery query)
        {
            var limitedRulesResult = await customerService.CheckAllLimitedRules(query.ViewerId, query.ViewerLevelId, query.ActorUsername, query.Language);

            var viewModel = limitedRulesResult.IsFailed
                ? new CreateCustomerViewModel(query, true, limitedRulesResult.Message)
                : new CreateCustomerViewModel(
                query,
                new AppConfiguration(GetApplicationPath(), systemId),
                CDNUri,
                GetAssetsVersion(),
                GetHashKey(),
                await customerService.GetSuggestUsername(query.CustName, query.ViewerLevelId));

            return View(
                HttpContext.IsDebuggingEnabled
                    ? "~/Views/Customer/Create-Debug.cshtml"
                    : "~/Views/Customer/Create.cshtml", viewModel);
        }

        public ActionResult EditCustomer(EditCustomerQuery query)
        {
            var viewModel = new EditCustomerViewModel(
                query,
                new AppConfiguration(GetApplicationPath(), systemId),
                CDNUri,
                GetAssetsVersion());

            return View(
              HttpContext.IsDebuggingEnabled
                  ? "~/Views/Customer/EditCustomer-Debug.cshtml"
                  : "~/Views/Customer/EditCustomer.cshtml", viewModel);
        }

        public async Task<ActionResult> AccountInfo(AccountInfoQuery query)
        {
            var accountInfo = await customerService.GetAccountInfo(query.CustId);
            return View("~/Views/Customer/AccountInfo.cshtml", new AccountInfoViewModel
            {
                Credit = accountInfo.MaxCredit,
                Usernames = accountInfo.UserName,
                Domains = accountInfo.domains
            });
        }

        public ActionResult EditMultiple(EditMultipleCustomersQuery query)
        {
            var viewModel = new EditMultipleCustomersViewModel(
                query,
                new AppConfiguration(GetApplicationPath(), systemId),
                CDNUri,
                GetAssetsVersion());

            return View(HttpContext.IsDebuggingEnabled
                  ? "~/Views/Customer/EditMultiple-Debug.cshtml"
                  : "~/Views/Customer/EditMultiple.cshtml", viewModel);
        }

        public ActionResult SportBookInformation(SportBookInformationQuery query)
        {
            var viewModel = new SportBookInformationViewModel(
                query,
                new AppConfiguration(GetApplicationPath(), systemId),
                CDNUri,
                GetAssetsVersion()
            );

            return View(
                HttpContext.IsDebuggingEnabled
                    ? "~/Views/Customer/SportBookInformation-Debug.cshtml"
                    : "~/Views/Customer/SportBookInformation.cshtml", viewModel);
        }

        private string GetHashKey()
        {
            var hashKey = encryptor.RandomString(4);
            sessionManager.Add(HashKeySessionKey, hashKey);

            return hashKey;
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Create(CreateCustomerRequest request)
        {
            if (request.IsInternal || !request.IsValid())
            {
                return UnAuthorized();
            }

            var generalInfo = request.GeneralInfo;
            var rawPassword = aesService.Decrypt(generalInfo.Password, sessionManager.Get<string>(HashKeySessionKey));
            generalInfo.Password = encryptor.CfsEnCode(rawPassword);
            generalInfo.UpLineRoleId = request.ViewerLevelId;
            generalInfo.Site = request.Site;
            generalInfo.SiteLabel = AppSettings.SiteLabel;

            var response = await customerService.CreateCustomer(new CreateCustomerParam(
                generalInfo,
                request.ProductGroups,
                request.ViewerId,
                request.SubAccId,
                request.IP,
                request.Site,
                AppSettings.SiteLabel,
                request.CustDBGroupId,
                request.IsDirectMember,
                request.NumberOfAccounts,
                CreationModes.FullMode,
                request.Language));

            return Content(response);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Update(UpdateCustomerRequest request)
        {
            if (request.IsInternal)
            {
                return UnAuthorized();
            }

            var response = await customerService.UpdateCustomer(
                new UpdateCustomerParam(
                    request.GeneralInfo,
                    request.ProductGroups,
                    request.ViewerId,
                    request.SubAccId,
                    request.CustId,
                    request.CustName,
                    request.RoleId,
                    request.UplineRoleId,
                    request.IP,
                    request.CustDBGroupId,
                    request.Site,
                    AppSettings.SiteLabel,
                    isDirectMemner: request.IsDirectMember,
                    language: request.Language));

            return Content(response);
        }

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateMultipleCustomers(UpdateMultipleCustomerRequest request)
        {
            if (request.IsInternal)
            {
                return UnAuthorized();
            }

            var response = await customerService.UpdateMultipleCustomers(
                new UpdateMultipleCustomerParam(
                    request.ProductGroups,
                    request.ViewerId,
                    request.SubAccId,
                    request.CustId,
                    request.CustName,
                    request.RoleId,
                    request.UplineRoleId,
                    request.IP,
                    request.CustDBGroupId,
                    request.CustomerIds,
                    request.CustomerNames,
                    request.Site,
                    AppSettings.SiteLabel,
                    request.Group,
                    request.Language));

            return Content(response);
        }

        public async Task<ActionResult> SearchSuggestDirectMembers(DirectMemberSuggestionQuery request)
            => Json(await customerService.SearchSuggestDirectMembers(request.ViewerId, request.SearchUsername), JsonRequestBehavior.AllowGet);

        public async Task<int> CheckDirectMemberUserName(DirectMemberSuggestionQuery request)
            => await customerService.CheckDirectMemberUserName(request.ViewerId, request.SearchUsername);

        public async Task<ActionResult> GetCustomerSetting(GetCustomerSettingRequest request)
            => Content(await customerService.GetCustomerSetting(request.CustId, request.RoleId, systemId, request.UplineRoleId, request.Language, request.IsGetUpLine, request.ProductGroupId, request.SiteLabel, isDirectMember: request.IsDirectMember));

        public async Task<ActionResult> GetCurrentWinLossAmount(CustomerWinLossAmountQuery request)
         => Content(await customerService.GetCustomerCurrentWinLossAmounts(request.CustId, request.SportType, request.BetType, request.Language));

        public async Task<ActionResult> GetPTSettingInformation(SportBookInformationQuery request)
            => Content(await customerService.GetPTSettingInformation(request.CustId, AppSettings.LocalTest, request.Site, request.Language));

        public async Task<ActionResult> GetMaxPayoutSettingInformation(SportBookInformationQuery request)
            => Content(await customerService.GetMaxPayoutSettingInformation(request.Site, request.SiteLabel, request.Language));

        public async Task<ActionResult> GetProductCategories(SportBookInformationQuery request)
            => Content(await customerService.GetProductCategories(request.CustId, request.Site, AppSettings.SiteLabel, request.Currency, request.Language));

        public async Task<ActionResult> GetCommissionHint(SportBookInformationQuery request)
            => Content(await customerService.GetCommissionHint(request.CustId, request.Site, request.Language));

        public async Task<ActionResult> CheckExistedCustomer(string username)
           => Json(await customerService.CheckExistedCustomer(username), JsonRequestBehavior.AllowGet);

        public async Task<ActionResult> GetWinLossLimitInformation(SportBookInformationQuery request)
           => Content(await customerService.GetWinLossLimitInformation(request.CustId, request.Site, AppSettings.SiteLabel, request.Currency, request.Language));

        private string GetApplicationPath()
        {
            var applicationPath = Response.ApplyAppPathModifier(Request.ApplicationPath);
            if (!applicationPath.EndsWith("/"))
            {
                applicationPath += "/";
            }

            return applicationPath;
        }

        private string GetAssetsVersion()
        {
            ObjectCache cache = MemoryCache.Default;
            var version = cache.Get(AssetsVersionCacheKey) as string;
            if (string.IsNullOrWhiteSpace(version))
            {
                version = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                cache.Set(AssetsVersionCacheKey, version, DateTime.Now.AddHours(4));
            }
            return version;
        }

        private ActionResult UnAuthorized()
          => Content(JsonConvert.SerializeObject(Result.CreateErrorResult(Lang.NotAuthorized), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}
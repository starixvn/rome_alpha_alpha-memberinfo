namespace Alpha.MemberInfo.Site.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;
    using System.Web;
    using System.Web.Mvc;
    using Alpha.CopyAccount.App_Start;
    using Alpha.MemberInfo.Features.CreateAccount;
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.AppResources.ProductResources;
    using Alpha.MemberInfo.Features.DormantAccountList;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Services.IndirectLoginAccount;
    using Alpha.MemberInfo.Site.Features.DormantAccountList;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.Shared.Helpers;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.PartialUM.Helpers;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.ViewModels;
    using Alpha.SecurityCode.Services;
    using Alpha.Web.Helpers;
    using Alpha.Web.Helpers.Impl;
    using AlphaPlus.PartialUM.DataAccess;
    using AlphaPlus.PartialUM.Helpers;
    using AlphaPlus.PartialUM.Services;
    using AlphaPlus.User.Models;
    using AlphaPlus.Utilities.IP;
    using Controllers;
    using Fanex.Caching;
    using Fanex.Cryptography.Aes;
    using Fanex.Customer.Client.Configurations;
    using Fanex.Customer.Client.NETFramework;
    using Fanex.Customer.Client.Services;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using MediatR;
    using MemberInfo.Models;
    using Models;
    using ONEbook.SpreadSheets;
    using ONEbook.SpreadSheets.NPOI;
    using Services;
    using Services.CreditBalance;
    using Services.MemberRequest;
    using Services.Transfer;
    using Services.WinLossLimit;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Utilities;

    public static class DependencyConfig
    {
        public static void Init()
        {
            RegisterQRCodeConfiguration();

            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            StartConfiguration(container);
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(SiteBaseController).Assembly;
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Singleton);
            container.Register<IPartialUMDataAccess, PartialUMDataAccess>(Lifestyle.Singleton);
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterSingleton<BrowserHelper, BrowserHelperImpl>();
            UserSession.Init(new HttpContextHelper());
            container.RegisterSingleton<IUserSession, AlphaPlus.User.Models.UserSessionExt>();
            container.RegisterSingleton<IUser, User>();
            container.RegisterPerWebRequest<TransferService, TransferServiceImpl>();
            container.RegisterSingleton<IPartialUMSectionHelper, PartialUMSectionHelper>();
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<CacheRefService, CacheRefServiceImpl>();
            container.RegisterPerWebRequest<IPService, IPServiceImpl>();
            container.RegisterSingleton<SecurityCodeChecker, SecurityCodeCheckerImpl>();

            container.RegisterPerWebRequest<MemberRequestService, MemberRequestServiceImpl>();
            container.Register<WinLossLimitService>(
                () => new WinLossLimitServiceImpl(
                    container.GetInstance<IDynamicRepository>(),
                    container.GetInstance<CacheRefService>().GetProductName));

            container.Register<CheckUMService, CheckUMServiceImpl>();

            container.RegisterPerWebRequest<CreditBalanceService, CreditBalanceServiceImpl>();
            container.RegisterPerWebRequest<IProblemAccountService, ProblemAccountService>();
            container.RegisterPerWebRequest<ProblemAccountWorkbook, ProblemAccountWorkbookImpl>();
            container.RegisterPerWebRequest<QrCodeAdapter, QrCodeAdapterImpl>();
            container.Register(typeof(Workbook<>), typeof(NpoiWorkbook<>), Lifestyle.Scoped);
            container.RegisterPerWebRequest<QRCodeMemberSignInService, QRCodeMemberSignInServiceImpl>();
            container.RegisterPerWebRequest<Encryptor, EncryptorImpl>();
            container.RegisterPerWebRequest<PasswordService, PasswordServiceImpl>();

            container.RegisterPerWebRequest<IndirectLoginAccountService, IndirectLoginAccountServiceImpl>();
            container.RegisterPerWebRequest<SessionManager, SessionManagerImpl>();
            container.RegisterSingleton(() => AesService.Default);

            container.RegisterPerWebRequest<CustomerListService, CustomerListServiceImpl>();
            container.RegisterPerWebRequest<UsernameSuggestionService, UsernameSuggestionServiceImpl>();

            container.RegisterPerWebRequest<DormantAccountService, DormantAccountServiceImpl>();
            container.RegisterPerWebRequest<DormantAccountWorkbook, DormantAccountWorkbookImpl>();
            container.RegisterCopyAccountServices("CloneAccount:BaseUri", "CloneAccount:ApiKey");

            container.RegisterPerWebRequest<CheckBelongingService, CheckBelongingServiceImpl>();

            container.RegisterPerWebRequest<MyWatchlistAccountsService, MyWatchlistAccountsServiceImpl>();
            container.RegisterPerWebRequest<ReachedThresholdService, ReachedThresholdServiceImpl>();
            container.RegisterPerWebRequest<ReachedThresholdWorkbook, ReachedThresholdWorkbookImpl>();
            container.RegisterPerWebRequest<MyThresholdsService, MyThresholdsServiceImpl>();
            container.RegisterSingleton<IPartialUMService, PartialUMService>();

            container.RegisterSingleton<ICacheService>(new CacheService());
            container.Register<IWinlossService, WinlossService>(Lifestyle.Scoped);
            container.RegisterPerWebRequest<IProductSettingInfoService, ProductSettingInfoService>();

            container.RegisterSingleton(new CustomerClientConfiguration().Initialize());
            container.RegisterSingleton<ICustomerService, CustomerService>();
        }

        private static void StartConfiguration(Container container)
        {
            SimpleServiceLocator
               .Instance
               .Register<Func<int, string>>(new CacheRefServiceImpl(container.GetInstance<IRemotingRefService>()).GetProductName)
               .Register<Func<string, string>>(ProductResources.ResourceManager.GetString)
               .Register<Func<string, string, string>>(MicroSites.GetMicroSiteUrl)
               .Register<Func<string, ResourceManager, string>>(ResourcesJsonConverter.ToConvert);
        }

        private static void RegisterQRCodeConfiguration()
        {
            var productConfigFile = HttpContext.Current.Server.MapPath(
                    AppSettingsHelper.GetProductConfigsFileRelativePath());

            var qrCodeconfigFilePath = HttpContext.Current.Server.MapPath(
                    AppSettingsHelper.GetQRCodeMemberSiteLoginConfigsPath());

            SimpleServiceLocator
                .Instance
                .Register<IConfigurationLoader>(new SearchAccountConfigurationLoader(new ProductJsonConverter(), productConfigFile))
                .Register<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>(new StatusViewModelFactory())
                .Register<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>(new SettingViewModelFactory())
                .Register<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel>>(new SettingMultipleViewModelFactory())
                .Register<QRCodeLoginConfigsHelper>(new QRCodeLoginConfigsHelperImpl(qrCodeconfigFilePath));
        }
    }
}
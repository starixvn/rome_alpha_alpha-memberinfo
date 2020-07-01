namespace Alpha.MemberInfo.Site.App_Start
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Services.IndirectLoginAccount;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.Products.Core.Services;
    using Alpha.Web.Helpers;
    using Alpha.Web.Helpers.Impl;
    using Controllers;
    using DataAccesses;
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
    using Services.ProgressiveJackpot;
    using Services.Transfer;
    using Services.WinLossLimit;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Casino;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using Sunplus.Agent.Utilities;
    using Utilities.IP;

    public static class DependencyConfig
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reviewed")]
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
            container.Register<WinLossLimitDataAccess, WinLossLimitDataAccessImpl>(Lifestyle.Singleton);
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterSingleton<IUserSession, UserSessionExt>();
            container.RegisterSingleton<IUser, User>();
            container.RegisterSingleton<IRemotingRefService, RemotingRefService>();
            container.RegisterSingleton<CacheRefService, CacheRefServiceImpl>();
            container.RegisterPerWebRequest<IPService, IPServiceImpl>();

            container.RegisterPerWebRequest<TransferService, TransferServiceImpl>();

            container.RegisterPerWebRequest<CasinoService, CasinoServiceImpl>();
            container.RegisterPerWebRequest<CasinoCustomer, CasinoCustomerImpl>();

            container.RegisterPerWebRequest<MemberRequestService, MemberRequestServiceImpl>();
            container.Register<WinLossLimitService>(
                () => new WinLossLimitServiceImpl(
                    container.GetInstance<IDynamicRepository>(),
                    container.GetInstance<WinLossLimitDataAccess>(),
                    container.GetInstance<CacheRefService>().GetProductName));

            container.Register<ProgressiveJackpotService>(
                () => new ProgressiveJackpotServiceImpl(
                    container.GetInstance<CacheRefService>(),
                    UserPermission.HasPermissionOnProduct),
                Lifestyle.Singleton);

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
            container.RegisterPerWebRequest<AesService, AesServiceImpl>();
        }

        private static void RegisterQRCodeConfiguration()
        {
            var qrCodeconfigFilePath = HttpContext.Current.Server.MapPath(
                    AppSettingsHelper.GetQRCodeMemberSiteLoginConfigsPath());

            SimpleServiceLocator
              .Instance
              .Register<QRCodeLoginConfigsHelper>(new QRCodeLoginConfigsHelperImpl(qrCodeconfigFilePath));
        }
    }
}
namespace Alpha.MemberInfo.Site
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using App_Start;
    using Auth.Filters;
    using Configurations;
    using Fanex.Data;
    using Fanex.Data.OldConnectionStringProvider;
    using Helpers;
    using ONEbook.UM.Filters;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Utilities;
    using Web;
    using Web.Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyConfig.Init();

            DbSettingProviderManager
                .StartNewSession()
                .Use(new OldConnectionStringProvider())
                .UseDefaultDbSettingProvider(
                    resourcePath: AppSettings.StoreProcedurePath,
                    enableWatching: true)
                .Run();

            AppSettings.Verify();

            ShareSession.Init();

            ModelBinders.Binders.DefaultBinder = new QueryEntityModelBinder();

            GlobalFilters.Filters.Add(new CheckUMFilter());
            GlobalFilters.Filters.Add(new CheckBelongingFilter());

            StaticResources.Init(GetBuildVersion());
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError().GetBaseException();
            var isIgnoreError = Request.IsLocal ||
                ex is HttpException ||
                ex.Message.Contains("Extend Token Error UserID");

            if (!isIgnoreError)
            {
                Logs.FilterInfoWarnOrError(ex, UserSession.Current.CustId);
            }
        }

        private static string GetBuildVersion()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return versionInfo.FileVersion;
        }
    }
}
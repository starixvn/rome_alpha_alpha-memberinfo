namespace Alpha.MemberInfo.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services.Transfer;
    using Fanex.Data.Repository;

    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        internal static NameValueCollection AppSettingValues
        {
            get { return appSettingValues ?? ConfigurationManager.AppSettings; }

            set { appSettingValues = value; }
        }

        public static string StoreProcedurePath => GetConfigValue("StoredProcedurePath");

        public static string SiteLabel => GetConfigValue("SiteLabel");

        public static string SiteLabel789y => GetConfigValue("SiteLabel789y");

        public static bool IsSiteLabel789y => SiteLabel.Equals(SiteLabel789y);

        public static string GetConfigValue(string configName) => AppSettingValues[configName];

        public static void Verify()
        {
            Require(StoreProcedurePath, nameof(StoreProcedurePath));
        }

        private static void Require(string key, string keyName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ConfigurationErrorsException($"{keyName} is null or whitespace");
            }
        }

        public static string OldMainAppPath => GetConfigValue("OldMainAppPath");

        public static string MainAppPath => GetConfigValue("MainAppPath");

        public static string RemoteRefURL => GetConfigValue("RemoteRefURL");

        public static string TransferAppPath => GetConfigValue("TransferAppPath");

        public static string IndirectLoginURL
            => !string.IsNullOrWhiteSpace(GetConfigValue("IndirectLoginURL")) ?
                $"{GetConfigValue("IndirectLoginURL")}_Authorization/IndirectLogin.aspx" :
                $"//{HttpContext.Current.Request.Headers["Host"].Replace("mb.", "mb2.")}/ex-main/_Authorization/IndirectLogin.aspx";

        public static IEnumerable<int> IndirectLoginSuperIds
            => GetConfigValue("IndirectLogin:AllowSuperIds")
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select
            (
                id => string.IsNullOrWhiteSpace(id) ? 0 : ValueOfNotNullOrWhiteSpace(id)
            );

        public static bool IsAllowedIndirectLogin(int srecommend)
            => IndirectLoginSuperIds.Contains(srecommend) || IndirectLoginSuperIds.Any(id => id == int.MaxValue);

        public static byte SettingStatus
        {
            get
            {
                byte.TryParse(ConfigurationManager.AppSettings["SettingStatus"], out byte settingStatus);
                return settingStatus;
            }
        }

        private static int ValueOfNotNullOrWhiteSpace(string value)
            => value.Equals("*", StringComparison.OrdinalIgnoreCase) ? int.MaxValue : Convert.ToInt32(value);

        public static IEnumerable<string> NewCaptchaLogCurrencies => string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["NewCaptcha:LogCurrencies"])
            ? Enumerable.Empty<string>()
            : ConfigurationManager.AppSettings["NewCaptcha:LogCurrencies"].Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

        public static string JsVersion => GetConfigValue("JsVersion");

        public static bool LocalTest => GetConfigValue("LocalTest") == "1";

        public static string SiteGroup => GetConfigValue("SiteLabel") ?? string.Empty;

        public static bool IsMBCSite => !SiteGroup.Equals("789y", StringComparison.OrdinalIgnoreCase);

        public static int MaxAccountsCreatePerRequest => Convert.ToInt32(GetConfigValue("CreateNewAccount:MaxAccountPerRequest"));

        public static Uri CustomerApiUri => new Uri(GetConfigValue("CustomerApi:Uri"));

        public static bool IsStandaloneBetlist => !string.IsNullOrWhiteSpace(GetConfigValue("Betlist:Domain"));

        public static string BetlistDomain
        {
            get
            {
                var betlistDomain = string.Empty + GetConfigValue("Betlist:Domain");
                var mainDomain = HttpContext.Current?.Request?.Headers?["Host"];

                if (HttpContext.Current?.Session?.IsCookieless == true)
                {
                    mainDomain = mainDomain?.Replace($"{mainDomain?.Split('.')?.ElementAt(0)}.", string.Empty);
                }

                return betlistDomain.IndexOf("{0}", StringComparison.OrdinalIgnoreCase) != -1
                    ? string.Format(betlistDomain, mainDomain)
                    : betlistDomain;
            }
        }
    }
}
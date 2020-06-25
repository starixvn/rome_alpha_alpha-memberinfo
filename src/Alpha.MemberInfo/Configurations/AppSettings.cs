namespace Alpha.MemberInfo.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Linq;

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

        public static string IndirectLoginURL 
            => !string.IsNullOrWhiteSpace(GetConfigValue("IndirectLoginURL")) ? 
                $"{GetConfigValue("IndirectLoginURL")}_Authorization/IndirectLogin.aspx" : 
                $"//{HttpContext.Current.Request.Headers["Host"].Replace("mb.", "imb.")}/ex-main/_Authorization/IndirectLogin.aspx";

        public static IEnumerable<int> IndirectLoginSuperIds
            => GetConfigValue("IndirectLogin:AllowSuperIds")
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select
            (
                id => string.IsNullOrWhiteSpace(id) ? 0 : ValueOfNotNullOrWhiteSpace(id)
            );

        public static bool IsAllowedIndirectLogin(int srecommend)
            => IndirectLoginSuperIds.Contains(srecommend) || IndirectLoginSuperIds.Any(id => id == int.MaxValue);

        private static int ValueOfNotNullOrWhiteSpace(string value)
            => value.Equals("*", StringComparison.OrdinalIgnoreCase) ? int.MaxValue : Convert.ToInt32(value);
    }
}
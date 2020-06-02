namespace Alpha.MemberInfo.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

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
            => GetConfigValue("LocalTest") == "1" ? 
                $"{GetConfigValue("IndirectLoginURL")}_Authorization/IndirectLogin.aspx" : 
                $"//{HttpContext.Current.Request.Headers["Host"].Replace("mb.", "imb.")}/ex-main/_Authorization/IndirectLogin.aspx";
    }
}
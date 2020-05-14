namespace Alpha.MemberInfo.Site.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Fanex.Logging.Extensions.Web;
    using Sunplus.Core.Common.IPs;
    using UAParser;

    public static class RequestInfoHelper
    {
        public static RequestInfo GetRequestInfo(HttpRequest request, string ip, string browserSize)
        {
            var browserName = string.Empty;
            var platform = string.Empty;

            try
            {
                var userAgentParser = Parser.GetDefault();
                var clientInfo = userAgentParser.Parse(request.UserAgent ?? string.Empty);

                browserName = GetBrowserName(clientInfo.UserAgent, browserSize);
                platform = GetPlatform(clientInfo);
            }
            catch
            {
                ExceptionHandler.Handle(new Exception("Cannot get request info"), TraceEventType.Information);
            }

            return new RequestInfo(
                ip,
                request.ServerVariables["SERVER_NAME"],
                browserName,
                platform,
                GetCountryCode(ip));
        }

        private static string GetCountryCode(string ip)
        {
            var country = "Unknown";

            try
            {
                var infoIp = IpUtils.LocationFromIP(ip);
                country = infoIp.CountryLong == "Reserved" ? null : infoIp.CountryLong;
            }
            catch
            {
                ExceptionHandler.Handle(new Exception("Cannot get country info (" + ip + ")"), TraceEventType.Information);
            }

            return country;
        }

        private static string GetBrowserName(UserAgent userAgent, string browserSize)
        {
            var browser = $"{userAgent.Family} {userAgent.Major}.{userAgent.Minor}.{userAgent.Patch ?? "0"}";

            return string.IsNullOrWhiteSpace(browserSize)
                ? browser
                : browser + $", {browserSize}";
        }

        private static string GetPlatform(ClientInfo clientInfo)
        {
            var deviceInfo = clientInfo.Device;
            var device = $"{deviceInfo.Family}, IsCrawler: {deviceInfo.IsSpider}";

            var os = clientInfo.OS;
            var majorVersion = os.Major ?? "0";
            var minorVersion = os.Minor ?? "0";
            var osVersion = os.Patch ?? "0";
            var operateSystem = $"{os.Family} {majorVersion}.{minorVersion}.{osVersion}";

            return $"{operateSystem}, Device: {device}";
        }
    }
}
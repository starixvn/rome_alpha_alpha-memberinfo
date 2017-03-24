namespace Alpha.MemberInfo.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using MemberInfo.Configurations;

    public static class MicroSites
    {
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Reviewed!")]
        public static string OldMainUrl(string virtualPath = "~/")
        {
            return AppPathHelper.GetAppPath(AppSettings.OldMainAppPath, virtualPath);
        }
    }
}
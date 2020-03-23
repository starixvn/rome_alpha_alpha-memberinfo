namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels
{
    public class QRCodeLoginConfigs
    {
        public QRCodeLoginConfigs(
            string api,
            string domain,
            int siteId,
            int expireSeconds)
        {
            API = api;
            Domain = domain;
            SiteId = siteId;
            ExpireSeconds = expireSeconds;
        }

        public string API { get; set; }
        public string Domain { get; set; }
        public int SiteId { get; set; }
        public int ExpireSeconds { get; set; }
    }
}
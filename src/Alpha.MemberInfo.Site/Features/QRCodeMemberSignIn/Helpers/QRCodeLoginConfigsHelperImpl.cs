namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    public interface QRCodeLoginConfigsHelper
    {
        QRCodeLoginConfigs GetQRCodeMemberSiteLoginConfigs();

        bool LocalTest();
    }

    public class QRCodeLoginConfigsHelperImpl : QRCodeLoginConfigsHelper
    {
        private readonly string configPath;

        public QRCodeLoginConfigsHelperImpl(string configPath)
        {
            this.configPath = configPath;
        }

        public QRCodeLoginConfigs GetQRCodeMemberSiteLoginConfigs()
        {
            IDictionary<string, string> jsonObject = JObject.Parse(File.ReadAllText(configPath))
            .Properties()
            .ToDictionary(jproperty => jproperty.Name, jproperty => jproperty.Value.ToString());

            return new QRCodeLoginConfigs(
                jsonObject["API"],
                jsonObject["Domain"].Split(',').ToArray(),
                int.Parse(jsonObject["SiteId"]),
                int.Parse(jsonObject["ExpireSeconds"]));
        }

        public bool LocalTest() => ConfigurationManager.AppSettings["LocalTest"] == "1";
    }
}
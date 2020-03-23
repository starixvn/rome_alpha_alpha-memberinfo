namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    public interface QRCodeLoginConfigsHelper
    {
        QRCodeLoginConfigs GetQRCodeMemberSiteLoginConfigs();
    }

    public class QRCodeLoginConfigsHelperImpl : QRCodeLoginConfigsHelper
    {
        public QRCodeLoginConfigs GetQRCodeMemberSiteLoginConfigs()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/QRCodeMemberSiteLoginConfigs.json");
            IDictionary<string, string> jsonObject = JObject.Parse(File.ReadAllText(filePath))
            .Properties()
            .ToDictionary(jproperty => jproperty.Name, jproperty => jproperty.Value.ToString());

            return new QRCodeLoginConfigs(
                jsonObject["API"],
                jsonObject["Domain"],
                int.Parse(jsonObject["SiteId"]),
                int.Parse(jsonObject["ExpireSeconds"]));
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    public class CustomerBaseViewModel
    {
        protected JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public CustomerBaseViewModel(AppConfiguration appConfig, string cndUri, string assetsVersion)
        {
            AppConfig = appConfig != null ? JsonConvert.SerializeObject(appConfig, jsonSerializerSettings) : string.Empty;
            CdnUri = cndUri;
            AssetsVersion = assetsVersion;
        }

        public string AppConfig { get; }

        public string CdnUri { get; }

        public string AssetsVersion { get; }

        public string Params { get; protected set; }
    }
}
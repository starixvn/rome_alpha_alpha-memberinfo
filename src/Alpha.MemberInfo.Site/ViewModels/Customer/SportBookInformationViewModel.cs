namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    using Alpha.MemberInfo.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Alpha.MemberInfo.Site.Requests.Customer;

    public class SportBookInformationViewModel : CustomerBaseViewModel
    {
        public SportBookInformationViewModel(
            SportBookInformationQuery query,
            AppConfiguration appConfig,
            string cndUri,
            string assetsVersion) : base(appConfig, cndUri, assetsVersion)
        {
            Params = query.ToJson();
        }
    }
}
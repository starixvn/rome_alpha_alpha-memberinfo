using Alpha.MemberInfo.Site.Requests.Customer;
using Newtonsoft.Json;
using Alpha.MemberInfo.Models;

namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    public class CreateDirectMemberViewModel : CustomerBaseViewModel
    {
        public CreateDirectMemberViewModel(CreateCustomerQuery query, bool isLimitNewDownline, string message)
            : this(query, null, string.Empty, string.Empty, string.Empty)
        {
            IsLimitNewDownline = isLimitNewDownline;
            Message = message;
        }

        public CreateDirectMemberViewModel(
            CreateCustomerQuery query,
            AppConfiguration appConfig,
            string cndUri,
            string assetsVersion,
            string secretKey,
            string suggestUsername = "")
            : base(appConfig, cndUri, assetsVersion)
        {
            SecretKey = secretKey;
            Params = JsonConvert.SerializeObject(new
            {
                uplineId = query.CustId,
                uplineRoleId = query.CustLevelId,
                suggestUsername,
                productGroupId = 0
            }, jsonSerializerSettings);
            ActorLevel = query.ActorLevel;
        }

        public bool IsLimitNewDownline { get; }

        public string Message { get; }

        public string SecretKey { get; }

        public UserLevel ActorLevel { get; }
    }
}
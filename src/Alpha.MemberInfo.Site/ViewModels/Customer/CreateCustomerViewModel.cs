using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Configurations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Requests.Customer;
using Newtonsoft.Json;

namespace Alpha.MemberInfo.Site.ViewModels.Customer
{
    public class CreateCustomerViewModel : CustomerBaseViewModel
    {
        public CreateCustomerViewModel(CreateCustomerQuery query, bool isLimitNewDownline, string message)
            : this(query, null, string.Empty, string.Empty, string.Empty)
        {
            IsLimitNewDownline = isLimitNewDownline;
            Message = message;
        }

        public CreateCustomerViewModel(
            CreateCustomerQuery query,
            AppConfiguration appConfig,
            string cndUri,
            string assetsVersion,
            string secretKey,
            string suggestUsername = "")
            : base(appConfig, cndUri, assetsVersion)
        {
            SecretKey = secretKey;
            Params = JsonConvert.SerializeObject(new { uplineId = query.CustId, uplineRoleId = query.CustLevelId, suggestUsername }, jsonSerializerSettings);
            ActorId = query.ViewerId;
            SubAccId = query.SubAccId;
            ActorLevel = query.ActorLevel;
            Language = query.Language;
            IsDarkMode = query.IsDarkMode;
        }

        public string SecretKey { get; }

        public int SubAccId { get; }

        public int ActorId { get; }

        public UserLevel ActorLevel { get; }

        public string PageTitle => ActorLevel.IsSuperLevel() ? Resources.AddNewMaster : (ActorLevel.IsMasterLevel() ? Resources.AddNewAgent : Resources.AddNewMember);

        public bool IsCreateMember => ActorLevel.IsAgentLevel();

        public bool IsOfferLiteMode => AppSettings.IsOfferCreateAccountLiteMode;

        public string Language { get; }

        public bool IsDarkMode { get; }

        public bool IsLimitNewDownline { get; }

        public string Message { get; }
    }
}
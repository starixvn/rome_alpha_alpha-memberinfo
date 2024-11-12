using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Configurations;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels
{
    public class CreateAccountViewModel
    {
        public CreateAccountViewModel(
            string language, bool isDarkMode, bool isLimitNewDownline = false, string errorMessage = "",
            byte levelId = 0, string secretKey = "", string suggestUsername = "", int custId = 0)
        {
            Language = language;
            IsDarkMode = isDarkMode;
            IsLimitNewDownline = isLimitNewDownline;
            ErrorMessage = errorMessage;
            IsCreateMember = levelId == UserLevel.Agent.Value;
            var userLevel = Enumeration.FromValue<UserLevel>(levelId);
            CustLevel = userLevel.DisplayName;
            SecretKey = secretKey;
            PageTitle = userLevel.IsSuperLevel()
                ? Resources.AddNewMaster
                : (userLevel.IsMasterLevel() ? Resources.AddNewAgent : Resources.AddNewMember);
            Params = JsonConvert.SerializeObject(new { uplineId = custId, suggestUsername, uplineRoleId = levelId },
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            ActorLevel = userLevel;
        }

        public string ErrorMessage { get; }

        public bool IsLimitNewDownline { get; }

        public bool IsCreateMember { get; }

        public UserLevel ActorLevel { get; }

        public string CustLevel { get; }

        public string PageTitle { get; }

        public string SecretKey { get; }

        public string Language { get; }

        public bool IsDarkMode { get; }

        public string Params { get; }

        public string GetUserGuideLink()
        {
            if (AppSettings.SupportedNewHandbook)
            {
                var handbookPath = Constants.UserLevel.GetDisplayName(ActorLevel.GetDirectDownLineLevel().Value).ToLowerInvariant();
                return MicroSites.HowToUseUrl($"~/Default/GetBySlug?slug=create-new-{handbookPath}");
            }

            return AppSettings.IsSiteLabel789y
                ? MicroSites.MessageUrl("~/StaticMessage/Guide?id=179159")
                : MicroSites.HowToUseUrl("~/Index.aspx?linktomessagesite=179159");
        }
    }
}
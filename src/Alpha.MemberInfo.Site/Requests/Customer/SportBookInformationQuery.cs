namespace Alpha.MemberInfo.Site.Requests.Customer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Alpha.MemberInfo.Models;
    using Alpha.Web.Helpers;

    public class SportBookInformationQuery : MemberInfoBaseQuery
    {
        public SportBookInformationQuery(IUser viewer) : base(viewer)
        {
        }

        public byte RoleId { get; set; }
        public int TabIndex { get; set; }
        
        public int Currency { get; set; }
        public bool IsSiteLabel789Y { get; set; }

        internal string ToJson() => JsonConvert.SerializeObject(
            new
            {
                tabs = BuildTabs(),
                is789y = IsSiteLabel789Y,
                roleId = RoleId,
                isShowMyanmarOdds = viewer.IsShowMyanmarOdds
            }
            , new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


        private object BuildTabs()
        {
            return new[]
            {
                BetSettingTab,
                MaxPayoutSettingTab,
                PTSettingTab,
                PTCommissionSettingTab,
                ProductCategorySettingTab,
            };
        }

        private object BetSettingTab
            => new
            {
                name = "BetSetting",
                id = "BetSettingTab",
                index = 0,
                isActive = TabIndex == 0,
            };

        private object MaxPayoutSettingTab
            => new
            {
                name = "MaxPayoutSetting", // chua dich
                id = "MaxPayoutSettingTab", 
                alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetMaxPayoutSettingInformation"),
                index = 1,
                isActive = TabIndex == 1,
            };

        private object PTSettingTab
            => new
            {
                name = "PositionTaking",
                id = "PTSettingTab",
                alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetPTSettingInformation"),
                index = 2,
                isActive = TabIndex == 2,
            };

        private object PTCommissionSettingTab
            => new
            {
                name = "Commission",
                id = "CommissionHintTab",
                alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetCommissionHint"),
                index = 3,
                isActive = TabIndex == 3,
            };

        private object ProductCategorySettingTab
            => new
            {
                name = "ProductCategories",
                id = "ProductCategorySettingTab",
                alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetProductCategories"),
                index = 4,
                isActive = TabIndex == 4,
            };
    }
}
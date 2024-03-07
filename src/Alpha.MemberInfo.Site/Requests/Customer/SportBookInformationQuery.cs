namespace Alpha.MemberInfo.Site.Requests.Customer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Alpha.MemberInfo.Models;
    using Alpha.Web.Helpers;

    public class SportBookInformationQuery : MemberInfoBaseQuery
    {
        public SportBookInformationQuery(IUser viewer) : base(viewer) { }
        public byte RoleId { get; set; }
        public int TabIndex { get; set; }
        public int Currency { get; set; }

        internal string ToJson() => JsonConvert.SerializeObject(new
        {
            tabs = BuildTabs(),
            roleId = RoleId,
            isShowMyanmarOdds = viewer.IsShowMyanmarOdds
        }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


        private object BuildTabs()
        {
            return new[]
            {
                BetSettingTab,
                MaxPayoutSettingTab,
                PTSettingTab,
                PTCommissionSettingTab,
                ProductCategorySettingTab,
                WinLossLimitTab
            };
        }

        private object BetSettingTab => new
        {
            name = "BetSetting",
            id = "BetSettingTab",
            isActive = TabIndex == 0,
        };

        private object MaxPayoutSettingTab => new
        {
            name = "MaxPayout",
            id = "MaxPayoutSettingTab",
            alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetMaxPayoutSettingInformation"),
            isActive = TabIndex == 1,
        };

        private object PTSettingTab => new
        {
            name = "PositionTaking",
            id = "PTSettingTab",
            alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetPTSettingInformation"),
            isActive = TabIndex == 2,
        };

        private object PTCommissionSettingTab => new
        {
            name = "Commission",
            id = "CommissionHintTab",
            alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetCommissionHint"),
            isActive = TabIndex == 3,
        };

        private object ProductCategorySettingTab => new
        {
            name = "ProductCategories",
            id = "ProductCategorySettingTab",
            alphaUrl = MicroSites.MemberInfoUrl("~/Customer/GetProductCategories"),
            isActive = TabIndex == 4,
        };
        
        private object WinLossLimitTab => new
        {
            name = "WinLossLimit",
            id = "WinLossLimitTab",
            isActive = TabIndex == 5,
        };
    }
}
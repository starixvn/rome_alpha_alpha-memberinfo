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
        public string SiteLabel { get; set; }

        internal string ToJson() => JsonConvert.SerializeObject(new
        {
            tabs = BuildTabs(),
            roleId = RoleId,
            site = Site,
            siteLabel = SiteLabel,
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
            url = MicroSites.MemberInfoUrl("~/Customer/GetMaxPayoutSettingInformation?site=" + Site + "&siteLabel=" + SiteLabel),
            isActive = TabIndex == 1,
        };

        private object PTSettingTab => new
        {
            name = "PositionTaking",
            id = "PTSettingTab",
            url = MicroSites.MemberInfoUrl("~/Customer/GetPTSettingInformation?custId=" + CustId + "&site=" + Site),
            isActive = TabIndex == 2,
        };

        private object PTCommissionSettingTab => new
        {
            name = "Commission",
            id = "CommissionHintTab",
            url = MicroSites.MemberInfoUrl("~/Customer/GetCommissionHint?custId=" + CustId + "&site=" + Site),
            isActive = TabIndex == 3,
        };

        private object ProductCategorySettingTab => new
        {
            name = "ProductCategories",
            id = "ProductCategorySettingTab",
            url = MicroSites.MemberInfoUrl("~/Customer/GetProductCategories?custId=" + CustId + "&site=" + Site + "&currency=" + Currency),
            isActive = TabIndex == 4,
        };

        private object WinLossLimitTab => new
        {
            name = "WinLossLimit",
            id = "WinLossLimitTab",
            url = MicroSites.MemberInfoUrl("~/Customer/GetWinLossLimitInformation?custId=" + CustId + "&site=" + Site + "&currency=" + Currency),
            isActive = TabIndex == 5,
        };
    }
}
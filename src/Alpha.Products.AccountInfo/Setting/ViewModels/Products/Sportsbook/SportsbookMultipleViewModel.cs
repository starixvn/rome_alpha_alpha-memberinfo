namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public class SportsbookMultipleViewModel : SportsbookViewModel, ISettingItemMultipleViewModel
    {
        public SportsbookMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        public override string Text => request.GetResource("EditMultipleSportsbookText");

        public override Dictionary<string, string> QueryStringParamaters
        => new Dictionary<string, string>
        {
            ["roleId"] = SearchAccount.CusomerLevelValue.ToString(),
            ["lng"] = request.Language,
            ["isDarkMode"] = request.IsDarkMode.ToString().ToLowerInvariant(),
            ["custIds"] = request.GetData<string>("customerIds"),
            ["usernames"] = request.GetData<string>("usernames"),
        };

        protected override string BaseAddress => $"{MemberInfoUrl}Customer/EditMultiple";

        public override string Url => BaseAddress;

        public override string Icon => "icon-edit Enable";

        public override bool ShowGuideLine => false;

        public string GuideLineUrl => $"{OldMainUrl}MemberInformation/MultipleEditSportsBook/GuideLine";
    }
}
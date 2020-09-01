namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public class GoldDeluxeMultipleViewModel : GoldDeluxeViewModel, ISettingItemMultipleViewModel
    {
        public GoldDeluxeMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => request.GetResource("EditMultipleGoldDeluxeText");

        public override string Icon => "icon-edit Enable";

        protected override string BaseAddress 
            => $"{OldMainUrl}GoldDeluxeSetting/GoldDeluxe/EditSettingMultiple";

        public override string Url => BaseAddress;

        public override Dictionary<string, string> QueryStringParamaters
             => new Dictionary<string, string>
            {
                ["uplineId"] = Actor.Id.ToString(),
                ["uplineLevel"] = Actor.CusomerLevelValue.ToString(),
                ["customerLevel"] = SearchAccount.CusomerLevelValue.ToString(),
                ["actorId"] = Actor.Id.ToString(),
                ["customerIds"] = request.GetData<string>("customerIds"),
                ["usernames"] = request.GetData<string>("usernames"),
                ["level"] = SearchAccount.CusomerLevelValue.ToString(),
                ["subAccId"] = Actor.SubAccountId.ToString(),
                ["formType"] = "1",
                ["isGetMultipleDCS"] = "1",
                ["isForcingMode"] = "0"
            };
    }
}
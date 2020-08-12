namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using System.Collections.Generic;

    public class GoldDeluxeMultipleViewModel : GoldDeluxeViewModel, ISettingItemMultipleViewModel
    {
        public GoldDeluxeMultipleViewModel(ISearchAccountInfoRequest request) : base(request)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetResource("GoldDeluxeViewModelText")}";

        public override string Icon => "icon-edit Enable";

        protected override string BaseAddress 
            => $"{OldMainUrl}GoldDeluxeSetting/GoldDeluxe/EditSettingMultiple";

        protected override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["uplineId"] = Actor.Id.ToString(),
                ["uplineLevel"] = Actor.CusomerLevelValue.ToString(),
                ["customerLevel"] = SearchAccount.CusomerLevelValue.ToString(),
                ["actorId"] = Actor.Id.ToString(),
                ["customerIds"] = Request.GetData<string>("customerIds"),
                ["usernames"] = Request.GetData<string>("usernames"),
                ["level"] = SearchAccount.CusomerLevelValue.ToString(),
                ["subAccId"] = Actor.SubAccountId.ToString(),
                ["formType"] = "1",
                ["isGetMultipleDCS"] = "1",
                ["isForcingMode"] = "0"
            };
    }
}
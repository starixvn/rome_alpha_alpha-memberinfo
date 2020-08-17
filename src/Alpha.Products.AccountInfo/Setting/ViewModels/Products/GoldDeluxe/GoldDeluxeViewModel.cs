namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using System.Collections.Generic;

    public class GoldDeluxeViewModel : ProductSettingItemViewModel
    {
        public GoldDeluxeViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) : 
            base(request, productItem)
        {
        }

        protected override string BaseAddress 
            => $"{OldMainUrl}GoldDeluxeSetting/GoldDeluxe/EditSetting";

        public override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["uplineId"] = Actor.Id.ToString(),
                ["uplineLevel"] = Actor.CusomerLevelValue.ToString(),
                ["actorId"] = Actor.Id.ToString(),
                ["custId"] = SearchAccount.Id.ToString(),
                ["level"] = SearchAccount.CusomerLevelValue.ToString(),
                ["subAccId"] = Actor.SubAccountId.ToString(),
                ["formType"] = "1"
            };

        public override bool Visible => base.Visible && !Actor.ProductStatusOf(ProductId).Hidden;
    }
}
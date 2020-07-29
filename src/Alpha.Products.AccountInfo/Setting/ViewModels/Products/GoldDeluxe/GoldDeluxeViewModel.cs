namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;
    using static Alpha.Products.Core.Shared.ProductConstants;

    public class GoldDeluxeViewModel : ProductSettingItemViewModel
    {
        public GoldDeluxeViewModel(ISearchAccountInfoRequest request) : base(request, GoldDeluxe)
        {
        }

        protected override string BaseAddress 
            => $"{OldMainUrl}GoldDeluxeSetting/GoldDeluxe/EditSetting";

        protected override Dictionary<string, string> QueryStringParamaters
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
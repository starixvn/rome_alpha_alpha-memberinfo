namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using System.Collections.Generic;

    public class SportsbookViewModel : ProductSettingItemViewModel
    {
        public SportsbookViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        public override string Text => request.GetProductName(ProductId);

        public override string Icon
            => request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission)
            ? "icon-edit Enable"
            : "icon-eye View";

        public override bool Enabled => true;

        public override Dictionary<string, string> QueryStringParamaters
         => new Dictionary<string, string>
         {
             ["custId"] = SearchAccount.Id.ToString(),
             ["roleId"] = SearchAccount.CusomerLevelValue.ToString(),
             ["lng"] = request.Language,
             ["isDarkMode"] = request.IsDarkMode.ToString().ToLowerInvariant()
         };

        protected override string BaseAddress => $"{MemberInfoUrl}Customer/EditCustomer";
    }
}
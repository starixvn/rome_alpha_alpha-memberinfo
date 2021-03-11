namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using System.Collections.Generic;

    public class KenoViewModel : ProductSettingItemViewModel
    {
        public KenoViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => request.GetProductName(ProductId);

        protected override string BaseAddress
            => OldMainUrl + "customersetting/kenosetting/kenosetting/" + 
            (SearchAccount.IsMember
            ? $"{Actor.CustomerLevel.DisplayName}EditMember"
            : $"Edit{SearchAccount.CustomerLevel.DisplayName}");

        public override Dictionary<string, string> QueryStringParamaters
         => new Dictionary<string, string>
         {
             ["custId"] = SearchAccount.Id.ToString(),
             ["username"] = SearchAccount.Username,
             ["userId"] = Actor.Id.ToString(),
             ["SubAccId"] = Actor.SubAccountId.ToString(),
             ["isMultipleEdit"] = "0"
         };

        public override bool Visible => base.Visible && !Actor.ProductStatusOf(ProductId).Hidden;
    }
}
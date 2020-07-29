namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;
    using static Alpha.Products.Core.Shared.ProductConstants;

    public class KenoViewModel : ProductSettingItemViewModel
    {
        public KenoViewModel(ISearchAccountInfoRequest request) : base(request, Keno)
        {
        }

        public override string Text => Request.GetProductName(ProductId);

        protected override string BaseAddress
            => OldMainUrl + "customersetting/kenosetting/kenosetting/" + 
            (SearchAccount.IsMember
            ? $"{Actor.CustomerLevel.DisplayName}EditMember"
            : $"Edit{SearchAccount.CustomerLevel.DisplayName}");

        protected override Dictionary<string, string> QueryStringParamaters
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
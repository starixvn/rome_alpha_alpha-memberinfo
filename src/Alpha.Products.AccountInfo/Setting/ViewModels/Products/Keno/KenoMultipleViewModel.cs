namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public class KenoMultipleViewModel : KenoViewModel, ISettingItemMultipleViewModel
    {
        public KenoMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => request.GetResource("EditMultipleRNGKenoText");

        public override string Icon => "icon-edit Enable";

        protected override string BaseAddress
            => OldMainUrl + "customersetting/kenosetting/kenosetting/" + 
            (SearchAccount.IsMember
            ? $"{Actor.CustomerLevel.DisplayName}EditMember"
            : $"Edit{SearchAccount.CustomerLevel.DisplayName}");

        public override string Url => BaseAddress;

        public override Dictionary<string, string> QueryStringParamaters
         => new Dictionary<string, string>
         {
             ["custId"] = SearchAccount.Id.ToString(),
             ["username"] = SearchAccount.Username,
             ["userId"] = Actor.Id.ToString(),
             ["SubAccId"] = Actor.SubAccountId.ToString(),
             ["isMultipleEdit"] = "1"
         };

        public override bool Visible => base.Visible && !Actor.ProductStatusOf(ProductId).Hidden;
    }
}
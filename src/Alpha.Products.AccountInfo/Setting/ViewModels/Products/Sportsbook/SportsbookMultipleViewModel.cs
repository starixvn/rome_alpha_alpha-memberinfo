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

        public override string Text => $"Edit Multiple {Request.GetProductName(ProductId)}";

        public Dictionary<string, string> QueryStringParamaters
        => new Dictionary<string, string>
        {
            ["m"] = "1",
            ["custId"] = SearchAccount.Id.ToString(),
            ["user"] = SearchAccount.Username,
            [$"{SearchAccount.Upline.CustomerLevel.DisplayName}id"] = SearchAccount.Upline.Id.ToString()
        };

        protected override string BaseAddress
        {
            get
            {
                var useCase = $"{Actor.CustomerLevel.DisplayName}/EditMultiple{SearchAccount.CustomerLevel.DisplayName}s";

                return $"{OldMainUrl}_MemberInfo/PositionTaking/{useCase}.aspx";
            }
        }

        public override string Icon => "icon-edit Enable";
    }
}
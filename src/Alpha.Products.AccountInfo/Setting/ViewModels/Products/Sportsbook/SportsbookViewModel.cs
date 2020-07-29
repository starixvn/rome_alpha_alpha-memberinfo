namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;
    using static Alpha.Products.Core.Shared.ProductConstants;

    public class SportsbookViewModel : ProductSettingItemViewModel
    {
        public SportsbookViewModel(ISearchAccountInfoRequest request) : base(request, Sportsbook)
        {
        }

        public override string Text => Request.GetProductName(ProductId);

        protected override Dictionary<string, string> QueryStringParamaters
         => new Dictionary<string, string>
         {
             ["m"] = "0",
             ["custId"] = SearchAccount.Id.ToString(),
             ["user"] = SearchAccount.Username,
             [$"{SearchAccount.Upline.CustomerLevel.DisplayName}id"] = SearchAccount.Upline.Id.ToString()
         };

        protected override string BaseAddress
        {
            get
            {
                var useCase = $"{Actor.CustomerLevel.DisplayName}/Edit{SearchAccount.CustomerLevel.DisplayName}";

                return $"{OldMainUrl}_MemberInfo/PositionTaking/{useCase}.aspx";
            }
        }
    }
}
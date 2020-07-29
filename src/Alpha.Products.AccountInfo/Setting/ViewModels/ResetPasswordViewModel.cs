namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Shared;
    using Base;
    using System.Collections.Generic;

    public class ResetPasswordViewModel : SettingItemViewModel
    {
        public ResetPasswordViewModel(ISearchAccountInfoRequest request, int displayOrder = 6)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress => $"{MainUrl}Password/ResetMainAccountPassword";

        protected override Dictionary<string, string> QueryStringParamaters
          => new Dictionary<string, string>
          {
              ["custId"] = SearchAccount.Id.ToString(),
              ["username"] = SearchAccount.Username,
              ["custRoleId"] = SearchAccount.CusomerLevelValue.ToString(),
              ["isCasinoSync"] = Request.SearchAccount.ProductStatusOf(ProductConstants.RNGCasino).Synced ? "1" : "0"
          };

        public override bool Visible => Actor.IsMainAccount || Request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission);
    }
}
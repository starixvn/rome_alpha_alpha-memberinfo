namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public class ResetPasswordViewModel : SettingItemViewModel
    {
        public ResetPasswordViewModel(ISearchAccountInfoRequest request, int displayOrder = 6)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress => $"{MainUrl}Password/ResetMainAccountPassword";

        public override Dictionary<string, string> QueryStringParamaters
          => new Dictionary<string, string>
          {
              ["custId"] = SearchAccount.Id.ToString(),
              ["username"] = SearchAccount.Username,
              ["custRoleId"] = SearchAccount.CusomerLevelValue.ToString()
          };

        public override bool Visible => Actor.IsMainAccount || Request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission);

        public override bool AddEditTextToPopupTitle => false;
    }
}
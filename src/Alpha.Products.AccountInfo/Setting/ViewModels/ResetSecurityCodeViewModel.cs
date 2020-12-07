namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using Alpha.Products.AccountInfo.Requests;
    using Base;
    using System.Collections.Generic;

    public class ResetSecurityCodeViewModel : SettingItemViewModel
    {
        public ResetSecurityCodeViewModel(ISearchAccountInfoRequest request, int displayOrder = 99998)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress => $"{MainUrl}SecurityCode/ResetSecurityCode";

        public override Dictionary<string, string> QueryStringParamaters
          => new Dictionary<string, string>
          {
              ["custId"] = SearchAccount.Id.ToString(),
              ["username"] = SearchAccount.Username,
              ["popup"] = "0",
          };

        public override bool Visible =>
            base.Visible &&
            SearchAccount.IsNotMember &&
            Request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission) &&
            Request.GetData<bool>(DataKey.SearchAccount.IsEnableSecurityCode);

        public override bool AddEditTextToPopupTitle => false;
    }
}
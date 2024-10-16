namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public class EditInfoViewModel : SettingItemViewModel
    {
        public EditInfoViewModel(ISearchAccountInfoRequest request, int displayOrder = 1)
            : base(request, displayOrder)

        {
        }

        protected override string BaseAddress => $"{OldMainUrl}_AccountInfo/EditCustomerInfo.aspx";

        public override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["custId"] = SearchAccount.Id.ToString(),
                ["username"] = SearchAccount.Username
            };
    }
}
namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Base;
    using Requests;
    using System.Collections.Generic;

    public class ParlayMaxPayoutMultipleViewModel : SettingItemViewModel, ISettingItemMultipleViewModel
    {
        public ParlayMaxPayoutMultipleViewModel(ISearchAccountInfoRequest request, int displayOrder = 1003)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress => $"{MemberInfoUrl}ParlayMaxPayout/EditMultiple";

        public override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["CustIds"] = Request.GetData<string>("customerIds"),
                ["RoleId"] = SearchAccount.CusomerLevelValue.ToString()
            };

        public override string Text => Request.GetResource("ParlayMaxPayoutMultipleText");
    }
}
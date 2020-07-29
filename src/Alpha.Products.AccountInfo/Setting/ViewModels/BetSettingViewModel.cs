namespace Alpha.Products.AccountInfo.Setting.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public class BetSettingViewModel : SettingItemViewModel
    {
        public BetSettingViewModel(ISearchAccountInfoRequest request, int displayOrder = 2)
            : base(request, displayOrder)
        {
        }

        protected override string BaseAddress 
            => $"{OldMainUrl}_MemberInfo/BetSetting/EditBetSetting.aspx";

        protected override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["custId"] = SearchAccount.Id.ToString(),
                ["username"] = SearchAccount.Username
            };
    }
}
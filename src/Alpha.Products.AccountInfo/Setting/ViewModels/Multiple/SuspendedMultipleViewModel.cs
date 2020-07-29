namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using System.Collections.Generic;

    public class SuspendedMultipleViewModel : SettingItemViewModel, ISettingItemMultipleViewModel
    {
        private const int DisplayOrder = 1001;

        public SuspendedMultipleViewModel(ISearchAccountInfoRequest request) : base(request, DisplayOrder)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetResource("ConfirmSuspendedViewModelText")}";

        protected override string BaseAddress 
            => $"{AccountInfoUrl}UnsuspendDownlines/EditMultiple";

        protected override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["CustIds"] = Request.GetData<string>("customerIds"),
                ["RoleId"] = SearchAccount.CusomerLevelValue.ToString()
            };
    }
}
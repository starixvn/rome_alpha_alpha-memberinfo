namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using System.Collections.Generic;

    public class DoubleCommMultipleViewModel : SettingItemViewModel, ISettingItemMultipleViewModel
    {
        private const int DisplayOrder = 1000;

        public DoubleCommMultipleViewModel(ISearchAccountInfoRequest request) : base(request, DisplayOrder)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetResource("ConfirmDoubleCommViewModelText")}";

        protected override string BaseAddress 
            => $"{OldMainUrl}_AccountInfo/ComfirmMultiEditDoubleComm.aspx";

        protected override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["arCID"] = Request.GetData<string>("customerIds")
            };
    }
}
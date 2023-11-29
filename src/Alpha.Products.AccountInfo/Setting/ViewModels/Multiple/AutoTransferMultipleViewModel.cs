namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using System.Collections.Generic;
    using Alpha.Web.Helpers;

    public class AutoTransferMultipleViewModel : SettingItemViewModel, ISettingItemMultipleViewModel
    {
        private new const int DisplayOrder = 1005;

        public AutoTransferMultipleViewModel(ISearchAccountInfoRequest request) : base(request, DisplayOrder)
        {
        }

        public override string Text => Request.GetResource("EditMultipleAutoTransferText");

        protected override string BaseAddress
            => MicroSites.TransferUrl("~/AutoTransfer/UpdateMultiple");

        public override Dictionary<string, string> QueryStringParamaters
              => new Dictionary<string, string>
              {
                  ["SearchAccountIds"] = Request.GetData<string>("customerIds"),
                  ["Usernames"] = Request.GetData<string>("usernames"),
                  ["SearchAccountRoleId"] = SearchAccount.CusomerLevelValue.ToString()
              };
    }
}
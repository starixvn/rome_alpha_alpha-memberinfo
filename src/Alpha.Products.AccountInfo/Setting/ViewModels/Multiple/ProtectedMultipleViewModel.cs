namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;

    public class ProtectedMultipleViewModel : SettingItemViewModel, ISettingItemMultipleViewModel
    {
        private new const int DisplayOrder = 1003;

        public ProtectedMultipleViewModel(ISearchAccountInfoRequest request) : base(request, DisplayOrder)
        {
        }

        public override string Text => Request.GetResource("EditMultipleProtectedStatusText");

        protected override string BaseAddress
            => $"{AccountInfoUrl}ProtectedStatus/EditMultiple";

        public override string Url => BaseAddress;

        public override Dictionary<string, string> QueryStringParamaters
           => new Dictionary<string, string>
           {
               ["CustIds"] = Request.GetData<string>("customerIds")
           };

        public override bool Visible => base.Visible && Request.SearchAccount.IsNotMember;
    }
}

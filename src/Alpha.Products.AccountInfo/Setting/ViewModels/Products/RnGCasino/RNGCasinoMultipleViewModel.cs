namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using System.Collections.Generic;

    public class RNGCasinoMultipleViewModel : RNGCasinoViewModel, ISettingItemMultipleViewModel
    {
        public RNGCasinoMultipleViewModel(ISearchAccountInfoRequest request) : base(request)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetResource("RNGCasinoViewModelText")}";

        public override string Icon => "icon-edit Enable";

        public override Dictionary<string, string> QueryStringParamaters => new Dictionary<string, string>();

        public Dictionary<string, string> QueryStringParamatersMultiple => base.QueryStringParamaters;
    }
}
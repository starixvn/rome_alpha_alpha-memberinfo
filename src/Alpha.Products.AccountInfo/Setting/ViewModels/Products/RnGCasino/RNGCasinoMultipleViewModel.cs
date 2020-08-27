namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public class RNGCasinoMultipleViewModel : RNGCasinoViewModel, ISettingItemMultipleViewModel
    {
        public RNGCasinoMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => Request.GetResource("EditMultipleCasinoText");

        public override string Icon => "icon-edit Enable";

        public override Dictionary<string, string> QueryStringParamaters => new Dictionary<string, string>();

        public Dictionary<string, string> QueryStringParamatersMultiple => base.QueryStringParamaters;
    }
}
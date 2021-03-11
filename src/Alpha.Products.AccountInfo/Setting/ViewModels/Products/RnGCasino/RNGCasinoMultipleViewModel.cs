namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public class RnGCasinoMultipleViewModel : RnGCasinoViewModel, ISettingItemMultipleViewModel
    {
        public RnGCasinoMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
            QueryStringParamaters = base.QueryStringParamaters;
            QueryStringParamaters.Add("syncedCasinoList", request.GetData<string>("syncedCasinoList"));
        }

        public override string Text => request.GetResource("EditMultipleCasinoText");

        public override string Icon => "icon-edit Enable";

        public override string Url => base.BaseAddress;

        public override Dictionary<string, string> QueryStringParamaters { get; }
    }
}
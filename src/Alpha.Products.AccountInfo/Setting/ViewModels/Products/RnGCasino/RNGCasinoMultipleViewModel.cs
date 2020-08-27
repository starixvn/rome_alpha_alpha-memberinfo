namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;

    public class RnGCasinoMultipleViewModel : RnGCasinoViewModel, ISettingItemMultipleViewModel
    {
        public RnGCasinoMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => Request.GetResource("EditMultipleCasinoText");

        public override string Icon => "icon-edit Enable";

        public override string Url => base.BaseAddress;
    }
}
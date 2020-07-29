namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;

    public class RNGCasinoMultipleViewModel : RNGCasinoViewModel, ISettingItemMultipleViewModel
    {
        public RNGCasinoMultipleViewModel(ISearchAccountInfoRequest request) : base(request)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetResource("RNGCasinoViewModelText")}";
    }
}
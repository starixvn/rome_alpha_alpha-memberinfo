namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.Core.Configuration;

    /// <summary>
    ///     View model NG2 DCS product
    /// </summary>
    public class Ng2ProductMultipleViewModel : Ng2ProductViewModel, ISettingItemMultipleViewModel
    {
        public Ng2ProductMultipleViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        public override string Url => base.BaseAddress;

        public override string Text => $"{Request.GetResource("EditMultiple")} {Request.GetProductName(productItem.ProductId)}";

        public override string Icon => "icon-edit Enable";
    }
}
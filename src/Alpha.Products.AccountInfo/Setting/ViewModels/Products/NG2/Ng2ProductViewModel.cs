namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Configurations;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Web.Helpers;

    /// <summary>
    ///     View model NG2 DCS product
    /// </summary>
    public class Ng2ProductViewModel : DynamicProductViewModel
    {
        public Ng2ProductViewModel(ISearchAccountInfoRequest request, int productId)
            : base(request, productId)
        {
        }

        protected override string BaseAddress 
            => $"{AppPathHelper.GetAppPath(AppSettings.Ng2AppPath, "~/")}dcs/viewcustomersetting";
    }
}
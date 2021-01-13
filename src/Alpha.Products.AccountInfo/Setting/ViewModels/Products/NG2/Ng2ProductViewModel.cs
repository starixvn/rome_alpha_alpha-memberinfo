namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;

    public class Ng2ProductViewModel : DynamicProductViewModel
    {
        public Ng2ProductViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        protected override string BaseAddress 
            => $"{Ng2Url}dcs/viewcustomersetting";
    }
}
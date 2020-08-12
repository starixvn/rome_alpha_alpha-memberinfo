namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using Alpha.Products.AccountInfo.Requests;

    public abstract class ProductSettingItemViewModel : SettingItemViewModel
    {
        private const int MaxSettingTypesOfAProduct = 5;
        protected readonly ISearchAccountInfoRequest Request;

        protected ProductSettingItemViewModel(ISearchAccountInfoRequest request, int productId)
            : base(request, productId * MaxSettingTypesOfAProduct)
        {
            ProductId = productId;
            Request = request;
        }

        internal int ProductId { get; }

        public override bool Visible => Actor.IsNotSuper || SearchAccount.IsNotAgent;

        public override string Icon
        {
            get
            {
                var customerStatus = Request.SearchAccount.ProductStatusOf(ProductId);
                if(!customerStatus.Synced)
                {
                    return "icon-refresh Synchronize";
                }

                return customerStatus.Enabled ? "icon-edit Enable" : "icon-edit Disable_edit";
            }
        }
    }
}
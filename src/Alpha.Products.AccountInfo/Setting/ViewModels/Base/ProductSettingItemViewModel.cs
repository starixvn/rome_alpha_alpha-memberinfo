namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;

    public abstract class ProductSettingItemViewModel : SettingItemViewModel
    {
        private const int MaxSettingTypesOfAProduct = 5;
        protected readonly ISearchAccountInfoRequest Request;
        protected readonly IConfigurationItem productItem;

        protected ProductSettingItemViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem.ProductId * MaxSettingTypesOfAProduct)
        {
            this.productItem = productItem;
            Request = request;
        }

        internal int ProductId => productItem.ProductId;

        public override bool Visible => Actor.IsNotSuper || SearchAccount.IsNotAgent;

        public virtual string Width => (productItem.ExtendProductProperty as SearchAccountProperties).Setting.Width;

        public virtual string Height => (productItem.ExtendProductProperty as SearchAccountProperties).Setting.Height;

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
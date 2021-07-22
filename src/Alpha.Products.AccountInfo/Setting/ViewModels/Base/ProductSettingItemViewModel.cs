namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Models;

    public abstract class ProductSettingItemViewModel : SettingItemViewModel
    {
        private const int MaxSettingTypesOfAProduct = 5;
        protected readonly ISearchAccountInfoRequest request;
        protected readonly IConfigurationItem productItem;
        protected readonly CustomerProductStatus customerStatus;

        protected ProductSettingItemViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem.ProductId * MaxSettingTypesOfAProduct)
        {
            this.productItem = productItem;
            this.request = request;
            this.customerStatus = request.SearchAccount.ProductStatusOf(productItem.ProductId);
        }

        public int ProductId => productItem.ProductId;

        public override bool Enabled => customerStatus.Enabled;

        public override bool Visible => Actor.IsNotSuper || SearchAccount.IsNotAgent;

        public override string Icon
        {
            get
            {
                if (!customerStatus.Synced)
                {
                    return "icon-refresh Synchronize";
                }

                if (!request.GetData<bool>(DataKey.Actor.HasUpdateAccountPermission))
                {
                    return "icon-eye View";
                }

                return customerStatus.Enabled ? "icon-edit Enable" : "icon-edit Disable_edit";
            }
        }
    }
}
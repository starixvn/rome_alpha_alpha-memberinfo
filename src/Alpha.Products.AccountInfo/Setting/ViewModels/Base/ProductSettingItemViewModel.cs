namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using Alpha.Products.AccountInfo.Requests;

    public abstract class ProductSettingItemViewModel : SettingItemViewModel
    {
        private const int MaxSettingTypesOfAProduct = 5;

        protected ProductSettingItemViewModel(ISearchAccountInfoRequest request, int productId)
            : base(request, productId * MaxSettingTypesOfAProduct)
        {
            ProductId = productId;
        }

        internal int ProductId { get; }

        public override bool Visible => Actor.IsNotSuper || SearchAccount.IsNotAgent;
    }
}
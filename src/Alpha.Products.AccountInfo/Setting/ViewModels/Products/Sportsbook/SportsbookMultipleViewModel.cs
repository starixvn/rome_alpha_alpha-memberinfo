namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;

    public class SportsbookMultipleViewModel : SportsbookViewModel, ISettingItemMultipleViewModel
    {
        public SportsbookMultipleViewModel(ISearchAccountInfoRequest request) : base(request)
        {
        }

        public override string Text => $"Edit Multiple {Request.GetProductName(ProductId)}";

        protected override string BaseAddress
        {
            get
            {
                var useCase = $"{Actor.CustomerLevel.DisplayName}/EditMultiple{SearchAccount.CustomerLevel.DisplayName}";

                return $"{OldMainUrl}_MemberInfo/PositionTaking/{useCase}.aspx";
            }
        }
    }
}
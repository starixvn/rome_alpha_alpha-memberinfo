namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using MemberInfo.Models.MemberRequests;

    public class ProductsViewModel
    {
        public ProductsViewModel(
            IEnumerable<PlayForFunProduct> products,
            int selectedProductId)
        {
            Products = products;
            SelectedProductId = selectedProductId;
        }

        public IEnumerable<PlayForFunProduct> Products { get; }

        public int SelectedProductId { get; }
    }
}
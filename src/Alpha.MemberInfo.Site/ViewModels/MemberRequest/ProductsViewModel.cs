namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using MemberInfo.Models;

    public class ProductsViewModel
    {
        public ProductsViewModel(
            IEnumerable<PlayForFunProduct> products,
            int selectedProductId)
        {
            Products = products;
            SelectedProductId = selectedProductId;
        }

        public IEnumerable<PlayForFunProduct> Products;

        public int SelectedProductId { get; }
    }
}
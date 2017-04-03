namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using Components;

    public class ProductsViewModel
    {
        public ProductsViewModel(
            int selectedProductId,
            IEnumerable<ProductViewModel> products)
        {
            Products = products;
            SelectedProductId = selectedProductId;
        }

        public int SelectedProductId { get; }

        public IEnumerable<ProductViewModel> Products { get; }

        public ProductMultiSelectionViewModel ProductMultiSelectionViewModel =>
           new ProductMultiSelectionViewModel(Products, SelectedProductId.ToString());
    }
}
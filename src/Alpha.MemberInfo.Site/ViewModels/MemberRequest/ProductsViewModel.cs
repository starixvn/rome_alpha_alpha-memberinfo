namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Components;

    public class ProductsViewModel
    {
        public ProductsViewModel(
            int selectedProductId,
            IEnumerable<SelectListItem> products)
        {
            Products = products;
            SelectedProductId = selectedProductId;
        }

        public int SelectedProductId { get; }

        public IEnumerable<SelectListItem> Products { get; }

        public ProductMultiSelectionViewModel ProductMultiSelectionViewModel =>
           new ProductMultiSelectionViewModel(Products, SelectedProductId.ToString());
    }
}
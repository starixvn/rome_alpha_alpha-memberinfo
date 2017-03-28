namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Components;
    using MemberInfo.Models;

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
namespace Alpha.MemberInfo.Site.ViewModels.Components
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ProductMultiSelectionViewModel
    {
        public ProductMultiSelectionViewModel(
            IEnumerable<SelectListItem> products,
            string userSelectedProductIds)
        {
            Products = products;
            UserSelectedProductIds = userSelectedProductIds;
        }

        public IEnumerable<SelectListItem> Products { get; }

        public string UserSelectedProductIds { get; }
    }
}
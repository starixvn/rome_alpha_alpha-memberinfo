namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using MemberInfo.Models;

    public class ProductsViewModel
    {
        public  IEnumerable<Product> Products;

        public ProductsViewModel(
            IEnumerable<Product> products,
            int selectedProductId)
        {
            Products = products;
            SelectedProductId = selectedProductId;
        }

        [Display(Name = "Product")]
        public int SelectedProductId { get; set; }

        //public IEnumerable<SelectListItem> Products
        //{
        //    get { return new SelectList(this.products, "Id", "Name", "Disabled"); }
        //}

        //public IEnumerable<SelectListItem> ClonedProducts
        //{
        //    get
        //    {
        //        return this.products.Select(product => new SelectListItem
        //        {
        //            Value = product.Id.ToString(),
        //            Text = product.Name
        //        });
        //    }
        //}
    }
}
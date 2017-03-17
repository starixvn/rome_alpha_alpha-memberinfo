namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using MemberInfo.Models;

    public class MemberRequestViewModel
    {
        private readonly IEnumerable<Product> products;

        public MemberRequestViewModel(
            IEnumerable<MemberRequestModel> memberRequestData,
            IEnumerable<Product> products,
            int selectedProductId)
        {
            MemberRequestData = memberRequestData;
            this.products = products.ToList();
            SelectedProductId = selectedProductId;
        }

        public IEnumerable<MemberRequestModel> MemberRequestData { get; }

        [Display(Name = "Product")]
        public int SelectedProductId { get; set; }

        public IEnumerable<SelectListItem> Products
        {
            get { return new SelectList(this.products, "Id", "Name"); }
        }

        public IEnumerable<SelectListItem> ClonedProducts
        {
            get
            {
                return this.products.Select(product => new SelectListItem
                {
                    Value = product.Id.ToString(),
                    Text = product.Name
                });
            }
        }
    }
}
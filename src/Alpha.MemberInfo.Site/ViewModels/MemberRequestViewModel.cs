namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Components;
    using MemberInfo.Models;

    public class MemberRequestViewModel
    {
        public MemberRequestViewModel(
            IEnumerable<MemberRequestModel> memberRequestData,
            IEnumerable<SelectListItem> products)
        {
            MemberRequestData = memberRequestData;
            Products = products;
        }

        public IEnumerable<MemberRequestModel> MemberRequestData { get; }

        public IEnumerable<SelectListItem> Products { get; }

        public string UserSelectedProductIds => "1,2,3,4,5,6,7";

        public ProductMultiSelectionViewModel ProductMultiSelectionViewModel =>
        new ProductMultiSelectionViewModel(Products, UserSelectedProductIds);
    }
}
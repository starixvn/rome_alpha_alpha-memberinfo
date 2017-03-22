namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using MemberInfo.Models;

    public class MemberRequestViewModel
    {
        public MemberRequestViewModel(
            IEnumerable<MemberRequestModel> memberRequestData,
            int selectedProductId,
            bool disabledUserSelectedProduct)
        {
            MemberRequestData = memberRequestData;
            SelectedProductId = selectedProductId;
            DisabledUserSelectedProduct = disabledUserSelectedProduct;
        }

        public IEnumerable<MemberRequestModel> MemberRequestData { get; }

        public int SelectedProductId { get; }

        public bool DisabledUserSelectedProduct { get; }
    }
}
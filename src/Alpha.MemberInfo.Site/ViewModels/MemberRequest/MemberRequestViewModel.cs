namespace Alpha.MemberInfo.Site.ViewModels.MemberRequest
{
    using System.Collections.Generic;
    using Constants;
    using MemberInfo.Helpers;
    using MemberInfo.Models.MemberRequests;

    public class MemberRequestViewModel
    {
        public MemberRequestViewModel(
            IEnumerable<MemberRequestModel> memberRequestData,
            int selectedProductId,
            bool disabledUserSelectedProduct,
            int actorId)
        {
            MemberRequestData = memberRequestData;
            SelectedProductId = selectedProductId;
            DisabledUserSelectedProduct = disabledUserSelectedProduct;
            ActorId = actorId;
        }

        public int ActorId { get; }

        public IEnumerable<MemberRequestModel> MemberRequestData { get; }

        public int SelectedProductId { get; }

        public bool DisabledUserSelectedProduct { get; }

        public string OldMainRootPath => MicroSites.OldMainUrl("~/");

        public string BuildProductSettingUrl(int productId, int custId, string custName)
        {
            switch (productId)
            {
                case ProductId.Casino:
                    return $"{OldMainRootPath}/CustomerSetting/Casino/Member/EditMember/{custId}/{ActorId}?{custName}";
            }

            return string.Empty;
        }
    }
}
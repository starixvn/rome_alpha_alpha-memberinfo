namespace Alpha.MemberInfo.Site.ViewModels.ProgressiveJackpot
{
    using System.Collections.Generic;
    using Components;
    using MemberInfo.Models.ProgressiveJackpot;

    public class ProgressiveJackpotViewModel
    {
        public ProgressiveJackpotViewModel(
            string userSelectedProductIds,
            IEnumerable<JackpotGameSetting> jackpotGameSettings,
            IEnumerable<ProductViewModel> products)
        {
            UserSelectedProductIds = userSelectedProductIds;
            JackpotGameSettings = jackpotGameSettings;
            Products = products;
        }

        public string UserSelectedProductIds { get; }

        public IEnumerable<JackpotGameSetting> JackpotGameSettings { get; }

        public IEnumerable<ProductViewModel> Products { get; }

        public ProductMultiSelectionViewModel ProductMultiSelectionViewModel => new ProductMultiSelectionViewModel(Products, UserSelectedProductIds);
    }
}
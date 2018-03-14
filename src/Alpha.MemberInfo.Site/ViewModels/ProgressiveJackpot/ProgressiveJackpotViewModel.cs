namespace Alpha.MemberInfo.Site.ViewModels.ProgressiveJackpot
{
    using System.Collections.Generic;
    using Components;
    using MemberInfo.Models.ProgressiveJackpot;
    using Requests.ProgressiveJackpot;

    public class ProgressiveJackpotViewModel
    {
        public ProgressiveJackpotViewModel(
            ProgressiveJacpotQuery query,
            IEnumerable<JackpotGameSetting> jackpotGameSettings,
            IEnumerable<ProductViewModel> products)
        {
            Language = query.Language;
            UserSelectedProductIds = query.UserSelectedProductIds;

            JackpotGameSettings = jackpotGameSettings;
            Products = products;
        }

        public string Language { get; }

        public string UserSelectedProductIds { get; }

        public IEnumerable<JackpotGameSetting> JackpotGameSettings { get; }

        public IEnumerable<ProductViewModel> Products { get; }

        public ProductMultiSelectionViewModel ProductMultiSelectionViewModel => new ProductMultiSelectionViewModel(Products, UserSelectedProductIds);
    }
}
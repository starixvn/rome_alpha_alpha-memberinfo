namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using System.Collections.Generic;

    public class RnGCasinoViewModel : ProductSettingItemViewModel
    {
        public RnGCasinoViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem)
            : base(request, productItem)
        {
        }

        private bool Unsynced => request.ActorIsEdittingDirectDownline && !SearchAccount.ProductStatusOf(ProductId).Synced;

        public override string Text => Unsynced ?
            request.GetResource($"Sync{GetType().Name}Text") :
            request.GetResource($"{GetType().Name}Text");

        protected override string BaseAddress
        {
            get
            {
                var searchAccountLevel = SearchAccount.CustomerLevel.DisplayName;
                var editBy = SearchAccount.IsMember && SearchAccount.IsNotDirectDownlineOf(Actor)
                    ? $"by{Actor.CustomerLevel.DisplayName}"
                    : string.Empty;

                return $"{OldMainUrl}CustomerSetting/Casino/{searchAccountLevel}/Edit{searchAccountLevel}{editBy}";
            }
        }

        public override Dictionary<string, string> QueryStringParamaters
            => new Dictionary<string, string>
            {
                ["id"] = SearchAccount.Id.ToString(),
                ["uplineid"] = Actor.Id.ToString(),
                ["username"] = SearchAccount.Username
            };

        public override bool AddEditTextToPopupTitle => !Unsynced;

        public override bool Visible
        {
            get
            {
                if (!base.Visible)
                {
                    return false;
                }

                if (request.ActorIsEdittingDirectDownline)
                {
                    return Actor.ProductStatusOf(ProductId).Synced;
                }

                if (Actor.ProductStatusOf(ProductId).Synced)
                {
                    return SearchAccount.ProductStatusOf(ProductId).Synced && Actor.ProductStatusOf(ProductId).Enabled;
                }

                return false;
            }
        }
    }
}
namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Base;
    using static Alpha.Products.Core.Shared.ProductConstants;

    public class RNGCasinoViewModel : ProductSettingItemViewModel
    {
        public RNGCasinoViewModel(ISearchAccountInfoRequest request, IConfigurationItem productItem) 
            : base(request, productItem)
        {
        }

        public override string Text => 
            Request.ActorIsEdittingDirectDownline && !SearchAccount.ProductStatusOf(ProductId).Synced ?
            Request.GetResource($"Sync{GetType().Name}Text") :
            Request.GetResource($"{GetType().Name}Text");

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

        public override bool Visible
        {
            get
            {
                if (!base.Visible)
                {
                    return false;
                }

                if (Request.ActorIsEdittingDirectDownline)
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
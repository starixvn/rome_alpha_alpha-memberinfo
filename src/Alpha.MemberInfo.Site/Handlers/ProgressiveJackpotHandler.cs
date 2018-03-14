namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using MediatR;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;
    using Params;
    using Requests.ProgressiveJackpot;
    using Services.ProgressiveJackpot;
    using ViewModels;
    using ViewModels.ProgressiveJackpot;

    public class ProgressiveJackpotHandler : IRequestHandler<ProgressiveJacpotQuery, ProgressiveJackpotViewModel>
    {
        private readonly ProgressiveJackpotService progressiveJackpotService;

        public ProgressiveJackpotHandler(ProgressiveJackpotService progressiveJackpotService)
        {
            this.progressiveJackpotService = progressiveJackpotService;
        }

        public ProgressiveJackpotViewModel Handle(ProgressiveJacpotQuery message)
        {
            var products = progressiveJackpotService.GetJackpotProducts(message.EnableJackpotProductIds);

            var param = new ProgressiveJackpotParam(message.CustId, message.Language);

            var jackpotGameSettings = progressiveJackpotService.GetJackpotGameSettings(param);

            var userSelectedProductIds = string.IsNullOrWhiteSpace(message.UserSelectedProductIds)
                ? message.EnableJackpotProductIds
                : message.UserSelectedProductIds.ToListInt();

            return new ProgressiveJackpotViewModel(
                message,
                jackpotGameSettings,
                BuildProductsViewModel(userSelectedProductIds, products));
        }

        internal static IEnumerable<ProductViewModel> BuildProductsViewModel(IEnumerable<int> userSelectedProductIds, IEnumerable<Product> products)
        {
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductViewModel>();
            }

            return products.Select(
                product =>
                {
                    return new ProductViewModel(
                        product.Name,
                        product.Id.ToString(),
                        userSelectedProductIds.Contains(product.Id),
                        false,
                        new GetSettingResult(false, 0, string.Empty));
                });
        }
    }
}
namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using MemberInfo.Models;
    using Params;
    using Requests.MemberRequest;
    using Services;
    using ViewModels;
    using ViewModels.MemberRequest;

    public class MemberRequestHandler :
        IRequestHandler<ProductQuery, ProductsViewModel>,
        IRequestHandler<MemberRequestsQuery, MemberRequestViewModel>
    {
        private readonly MemberRequestService service;

        public MemberRequestHandler(MemberRequestService service)
        {
            this.service = service;
        }

        public ProductsViewModel Handle(ProductQuery message)
        {
            var products = service.GetPlayForFunProduct(message.CustId);
            var viewModel = new ProductsViewModel(
                message.UserSelectedProductId,
                BuildProductsViewModel(new List<int> { message.UserSelectedProductId }, products, message));

            return viewModel;
        }

        public MemberRequestViewModel Handle(MemberRequestsQuery message)
        {
            var param = new MemberRequestsParam(message.CustId, message.UserSelectedProductId);
            var memberRequestData = service.GetMemberRequest(param);

            var viewModel = new MemberRequestViewModel(
                memberRequestData,
                message.UserSelectedProductId,
                message.DisabledUserSelectedProduct,
                message.CustId,
                message.CanUpdateAccount);

            return viewModel;
        }

        protected internal IEnumerable<ProductViewModel> BuildProductsViewModel(
            IEnumerable<int> userSelectedProductIds,
            IEnumerable<Product> products,
            ProductQuery message)
        {
            if (products == null
                || !products.Any())
            {
                return Enumerable.Empty<ProductViewModel>();
            }

            return products.Select(product =>
            {
                var productSelectListItem = new ProductViewModel(
                    product.Name,
                    product.Id.ToString(),
                    userSelectedProductIds.Contains(product.Id),
                    product.IsDisable,
                    service.HasSetting(new ProductSettingParam(product.Id, message.CustId, message.CustName)));

                return productSelectListItem;
            });
        }
    }
}
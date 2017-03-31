namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MediatR;
    using MemberInfo.Models;
    using Params;
    using Requests.MemberRequest;
    using Services;
    using ViewModels.MemberRequest;

    public class MemberRequestHandler :
        IRequestHandler<ProductQuery, ProductsViewModel>,
        IRequestHandler<MemberRequestsQuery, MemberRequestViewModel>,
        IRequestHandler<EnableProductCommand, IEnumerable<Result>>
    {
        private readonly MemberRequestService service;

        public MemberRequestHandler(MemberRequestService service)
        {
            this.service = service;
        }

        public ProductsViewModel Handle(ProductQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var products = service.GetPlayForFunProduct(message.CustId);
            var viewModel = new ProductsViewModel(
                            message.UserSelectedProductId,
                            BuildProductSelectListItems(new List<int> { message.UserSelectedProductId }, products));

            return viewModel;
        }

        public MemberRequestViewModel Handle(MemberRequestsQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

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

        public IEnumerable<Result> Handle(EnableProductCommand message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var enableResult = service.EnableProduct(new EnableProductParam(
                message.UserSelectedProductId,
                message.Members,
                message.ViewerId,
                message.ViewerName,
                message.ViewerSubAccId,
                message.IP,
                message.Language));

            return enableResult;
        }

        protected internal IEnumerable<SelectListItem> BuildProductSelectListItems(
          IEnumerable<int> userSelectedProductIds,
          IEnumerable<Product> products)
        {
            if (products == null
                || !products.Any())
            {
                return Enumerable.Empty<SelectListItem>();
            }

            return products.Select(product =>
            {
                var productSelectListItem = new SelectListItem
                {
                    Text = product.Name,
                    Value = product.Id.ToString(),
                    Selected = userSelectedProductIds.Contains(product.Id),
                    Disabled = product.IsDisable
                };

                return productSelectListItem;
            });
        }
    }
}
namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Extensions;
    using MediatR;
    using MemberInfo.Models;
    using Params;
    using Requests.MemberRequest;
    using Services;
    using ViewModels.MemberRequest;

    public class MemberRequestHandler : IRequestHandler<MemberRequestQuery, MemberRequestViewModel>
    {
        private readonly MemberRequestService service;
        private readonly ProductService productService;

        public MemberRequestHandler(
            ProductService productService,
            MemberRequestService service)
        {
            this.productService = productService;
            this.service = service;
        }

        public MemberRequestViewModel Handle(MemberRequestQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var param = new MemberRequestParam();
            var products = productService.GetProductsForCustomer(message.CustId);
            var validUserSelectedProductIds = productService.FilterProductIdsByCustomer(
                                                message.CustId,
                                                message.UserSelectedProductIds.ToListInt());
            var memberRequestData = service.GetMemberRequest(param);

            var viewModel = new MemberRequestViewModel(memberRequestData, BuildProductSelectListItems(validUserSelectedProductIds, products));

            return viewModel;
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
                    Text = product.DisplayName,
                    Value = product.Id.ToString(),
                    Selected = userSelectedProductIds.Contains(product.Id)
                };

                return productSelectListItem;
            });
        }
    }
}
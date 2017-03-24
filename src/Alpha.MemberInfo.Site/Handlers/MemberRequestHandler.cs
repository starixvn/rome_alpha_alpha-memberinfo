namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using System.Collections.Generic;
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
            var products = service.SupportedProduct(message.CustId);
            var viewModel = new ProductsViewModel(products, message.UserSelectedProductId);
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

            var viewModel = new MemberRequestViewModel(memberRequestData, message.UserSelectedProductId, message.DisabledUserSelectedProduct, message.CustId);
            return viewModel;
        }

        public IEnumerable<Result> Handle(EnableProductCommand message)
        {
            return service.EnableProduct(new EnableProductParam(
                message.UserSelectedProductId,
                message.Members,
                message.ViewerId,
                message.ViewerName,
                message.ViewerSubAccId,
                string.Empty,
                "E"));
        }
    }
}
namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using System.Collections.Generic;
    using MediatR;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;
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
            var products = new List<PlayForFunProduct>
            {
                new PlayForFunProduct(6, "Casino", false),
                new PlayForFunProduct(24, "Fishing World", true),
                new PlayForFunProduct(14, "Macau Games", true)
            };

            ////var products = service.SupportedProduct(message.CustId);
            var viewModel = new ProductsViewModel(products, message.UserSelectedProductId);
            return viewModel;
        }

        public MemberRequestViewModel Handle(MemberRequestsQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var param = new MemberRequestsParam(message.ActorId, message.UserSelectedProductId);
            var memberRequestData = service.GetMemberRequest(param);

            var viewModel = new MemberRequestViewModel(memberRequestData, message.UserSelectedProductId, message.DisabledUserSelectedProduct);
            return viewModel;
        }

        public IEnumerable<Result> Handle(EnableProductCommand comand)
        {
            return service.EnableProduct(new EnableProductParam(
                comand.UserSelectedProductId,
                comand.Members,
                comand.ActorId,
                comand.ActorName,
                comand.ActorSubAccId,
                string.Empty,
                "E"));
        }
    }
}
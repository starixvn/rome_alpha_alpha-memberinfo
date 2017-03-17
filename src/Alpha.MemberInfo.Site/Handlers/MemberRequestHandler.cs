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

    public class MemberRequestHandler : IRequestHandler<MemberRequestQuery, MemberRequestViewModel>
    {
        private readonly MemberRequestService service;

        public MemberRequestHandler(MemberRequestService service)
        {
            this.service = service;
        }

        public MemberRequestViewModel Handle(MemberRequestQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var param = new MemberRequestParam();

            var products = new List<Product>
            {
                new Product(5, "Casino"),
                new Product(6, "Fishing World"),
                new Product(7, "Macau Games")
            };

            var memberRequestData = service.GetMemberRequest(param);
            var viewModel = new MemberRequestViewModel(memberRequestData, products, message.UserSelectedProductId);

            return viewModel;
        }
    }
}
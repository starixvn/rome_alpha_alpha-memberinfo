namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using MediatR;
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
            var memberRequestData = service.GetMemberRequest(param);

            var viewModel = new MemberRequestViewModel(memberRequestData);

            return viewModel;
        }
    }
}
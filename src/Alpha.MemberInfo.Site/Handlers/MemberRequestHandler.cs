namespace Alpha.MemberInfo.Site.Handlers.MemberRequest
{
    using System;
    using MediatR;
    using Requests.MemberRequest;
    using ViewModels.MemberRequest;

    public class MemberRequestHandler : IRequestHandler<MemberRequestQuery, MemberRequestViewModel>
    {
        public MemberRequestHandler()
        {
        }

        public MemberRequestViewModel Handle(MemberRequestQuery message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var viewModel = new MemberRequestViewModel(message);

            return viewModel;
        }
    }
}
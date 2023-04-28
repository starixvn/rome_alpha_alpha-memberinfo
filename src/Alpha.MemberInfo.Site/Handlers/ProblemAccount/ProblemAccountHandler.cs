namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using Alpha.Web.Helpers;
    using MediatR;

    public class ProblemAccountHandler :
        IRequestHandler<IndexRequest, ProblemAccountReportViewModel>,
        IRequestHandler<GetProblemAccountRequest, IEnumerable<ProblemAccountModel>>,
        IRequestHandler<UpdateProblemAccountCommand, Result>,
        IRequestHandler<UpdateProblemAccountRequest, UpdateReviewStatusViewModel>
    {
        private readonly IProblemAccountService service;
        private readonly BrowserHelper browserHelper;

        public ProblemAccountHandler(IProblemAccountService service, BrowserHelper browserHelper)
        {
            this.browserHelper = browserHelper;
            this.service = service;
        }

        public UpdateReviewStatusViewModel Handle(UpdateProblemAccountRequest message)
            => new UpdateReviewStatusViewModel(message);

        public Result Handle(UpdateProblemAccountCommand message)
        {
            return service.UpdateReviewStatus(new UpdateProblemAccountParam
            {
                CustId = message.CustId,
                CustomerList = message.Ids,
                IsAffectedAllDownlines = message.IsAffectedAllDownlines,
                Language = message.Language,
                LoginIp = message.IP,
                RoleId = message.RoleId,
                SubAccId = message.SubAccId
            });
        }

        public ProblemAccountReportViewModel Handle(IndexRequest message)
            => new ProblemAccountReportViewModel(message, new List<ProblemAccountModel>(),
                browserHelper.IsMobile(isOnlyTablet: false));

        public IEnumerable<ProblemAccountModel> Handle(GetProblemAccountRequest message)
        {
            var models = service.Get(CreateParam(message));

            return models;
        }

        protected internal static GetProblemAccountParam CreateParam(ProblemAccountRequestBase message)
            => new GetProblemAccountParam
            {
                FromDate = message.FromDate,
                ToDate = message.ToDate,
                ReviewStatusId = message.UserSelectedReviewedStatusId,
                CustId = message.CustId,
                SubAccId = message.CustId,
                UplineUserName = message.UplineName,
                SearchRoleId = message.SearchRoleId,
                SearchUserName = message.UserName
            };
    }
}
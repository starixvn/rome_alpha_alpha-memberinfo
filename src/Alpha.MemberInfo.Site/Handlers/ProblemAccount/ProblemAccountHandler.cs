namespace Alpha.MemberInfo.Site.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params;
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
        IRequestHandler<UpdateProblemAccountRequest, UpdateReviewStatusViewModel>,
        IRequestHandler<UpdateMultipleSuspendProblemAccountRequest, UpdateMultipleSuspendViewModel>,
        IAsyncRequestHandler<ConfirmAddToWatchlistQuery, ConfirmAddToWatchlistViewModel>
    {
        private readonly IProblemAccountService problemAccountService;
        private readonly MyWatchlistAccountsService myWatchlistAccountsService;
        private readonly BrowserHelper browserHelper;

        public ProblemAccountHandler(IProblemAccountService problemAccountService, MyWatchlistAccountsService myWatchlistAccountsService, BrowserHelper browserHelper)
        {

            this.browserHelper = browserHelper;
            this.problemAccountService = problemAccountService;
            this.myWatchlistAccountsService = myWatchlistAccountsService;
        }

        public UpdateReviewStatusViewModel Handle(UpdateProblemAccountRequest message)
            => new UpdateReviewStatusViewModel(message);

        public Result Handle(UpdateProblemAccountCommand message)
        {
            return problemAccountService.UpdateReviewStatus(new UpdateProblemAccountParam
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

        public UpdateMultipleSuspendViewModel Handle(UpdateMultipleSuspendProblemAccountRequest message)
            => new UpdateMultipleSuspendViewModel(message);

        public ProblemAccountReportViewModel Handle(IndexRequest message)
            => new ProblemAccountReportViewModel(message, new List<ProblemAccountModel>(),
                browserHelper.IsMobile(isOnlyTablet: false));

        public IEnumerable<ProblemAccountModel> Handle(GetProblemAccountRequest message)
        {
            var models = problemAccountService.Get(CreateParam(message));

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

        public async Task<ConfirmAddToWatchlistViewModel> Handle(ConfirmAddToWatchlistQuery message)
        {
            var param = new SelectMultipleWatchlistParam(message.CustIds, message.Site);
            var multipleWatchlistAccountModel = await myWatchlistAccountsService.GetMultipleWatchlistAccount(param);

            return new ConfirmAddToWatchlistViewModel(multipleWatchlistAccountModel);
        }
    }
}
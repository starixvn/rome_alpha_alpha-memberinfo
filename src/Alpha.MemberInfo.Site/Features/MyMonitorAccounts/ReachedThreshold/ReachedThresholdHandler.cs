namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold
{
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.ReachedThreshold.Params;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.ReachedThreshold.ViewModels;
    using Alpha.Web.Helpers;
    using MediatR;
    using System.Threading.Tasks;

    public class ReachedThresholdHandler : 
        IAsyncRequestHandler<ReachedThresholdRequest, ReachedThresholdViewModel>,
        IAsyncRequestHandler<ReachedThresholdQuery, ReachedThresholdPagingViewModel>,
        IAsyncRequestHandler<ReachHistoryQuery, ReachHistoryViewModel>
    {
        private readonly ReachedThresholdService reachedThresholdService;
        private readonly MyWatchlistAccountsService myWatchlistAccountsService;
        private readonly BrowserHelper browserHelper;

        public ReachedThresholdHandler(
            ReachedThresholdService reachedThresholdService, 
            MyWatchlistAccountsService myWatchlistAccountsService,
            BrowserHelper browserHelper)
        {
            this.reachedThresholdService = reachedThresholdService;
            this.myWatchlistAccountsService = myWatchlistAccountsService;
            this.browserHelper = browserHelper;
        }

        public async Task<ReachedThresholdViewModel> Handle(ReachedThresholdRequest message)
        {
            var isMobile = browserHelper.IsMobile(isOnlyTablet: false);
            var query = new ReachedThresholdQuery(message.Viewer)
            {
                Username = message.Username,
                FromDate = message.FromDate,
                ToDate = message.ToDate,
                Status = message.Status,
                LimitType = message.LimitType,
                SortType = message.SortType,
                IsDesc = message.IsDesc,
                PageIndex = message.PageIndex,
                PageSize = message.PageSize
            };

            var pagingViewModel = await Handle(query);

            var param = new SelectGlobalThresholdParam(message.CustId);
            var globalThreshold = await myWatchlistAccountsService.GetGlobalThreshold(param);

            return new ReachedThresholdViewModel(message, globalThreshold, isMobile, pagingViewModel);
        }

        public async Task<ReachedThresholdPagingViewModel> Handle(ReachedThresholdQuery message)
        {
            var param = new SelectReachListParam(
                message.CustId,
                message.Username,
                message.FromDate,
                message.ToDate,
                message.Status != 0 ? ReachedThresholdStatusConstants.GetReachedThresholdStatus(message.Status) : string.Empty,
                message.LimitType,
                message.SortType,
                message.IsDesc,
                message.PageSize,
                message.PageIndex);

            var reachlistAccountDTO = await reachedThresholdService.GetReachlistAccounts(param);

            return new ReachedThresholdPagingViewModel(reachlistAccountDTO);
        }

        public async Task<ReachHistoryViewModel> Handle(ReachHistoryQuery message)
        {
            var param = new SelectReachDetailParam(
                message.SelectedCustId,
                message.FromDate,
                message.ToDate,
                message.LimitType);

            var reachHistories = await reachedThresholdService.GetReachDetail(param);

            return new ReachHistoryViewModel(reachHistories);
        }
    }
}
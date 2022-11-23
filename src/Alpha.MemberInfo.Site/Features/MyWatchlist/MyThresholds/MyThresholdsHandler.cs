namespace Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.Requests;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyThresholds.ViewModels;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.Web.Helpers;
    using MediatR;

    public class MyThresholdsHandler :
        IAsyncRequestHandler<MyThresholdIndexPageQuery, MyThresholdIndexPageViewModel>,
        IAsyncRequestHandler<MyThresholdHistoryQuery, IEnumerable<ReachHistoryModel>>,
        IAsyncRequestHandler<MyThresholdQuery, ThresholdViewModel>,
        IAsyncRequestHandler<MyThresholdCommand, Result>
    {
        private readonly BrowserHelper browserHelper;
        private readonly MyThresholdsService myThresholdsService;

        public MyThresholdsHandler(
            BrowserHelper browserHelper,
            MyThresholdsService myThresholdsService)
        {
            this.browserHelper = browserHelper;
            this.myThresholdsService = myThresholdsService;
        }

        public async Task<MyThresholdIndexPageViewModel> Handle(MyThresholdIndexPageQuery message)
        {
            var isMobile = browserHelper.IsMobile(isOnlyTablet: false);
            var canUpdateAccount = message.CanUpdateAccount;
            var myThresholdTask = myThresholdsService.GetMyThreshold(message.ViewerId);
            var myThresholdHistoryTask = Handle((MyThresholdHistoryQuery)message);

            await Task.WhenAll(myThresholdTask, myThresholdHistoryTask);

            return new MyThresholdIndexPageViewModel(
                message,
                isMobile,
                canUpdateAccount,
                await myThresholdTask,
                await myThresholdHistoryTask);
        }

        public Task<IEnumerable<ReachHistoryModel>> Handle(MyThresholdHistoryQuery message)
        {
            var param = new SelectMyThresholdHistoryParam(
                message.ViewerId,
                message.FromDate,
                message.ToDate,
                message.SortType,
                message.IsDesc,
                message.LimitedDateToShowInfo);

            return myThresholdsService.GetMyThresholdHistory(param);
        }

        public async Task<ThresholdViewModel> Handle(MyThresholdQuery message)
        {
            var myThreshold = await myThresholdsService.GetMyThreshold(message.ViewerId);

            return myThreshold == null
                ? new ThresholdViewModel(null, null)
                : new ThresholdViewModel(myThreshold.WinningThreshold, myThreshold.LosingThreshold);
        }

        public Task<Result> Handle(MyThresholdCommand message)
            => myThresholdsService.UpdateMyThreshold(new UpdateMyThresholdParam(
                message.ViewerId,
                message.WinningThreshold,
                message.LosingThreshold,
                message.ViewerId,
                message.SubAccId,
                message.IP));
    }
}
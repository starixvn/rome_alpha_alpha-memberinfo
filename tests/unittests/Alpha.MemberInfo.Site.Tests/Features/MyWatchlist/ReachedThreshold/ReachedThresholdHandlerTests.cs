namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.ViewModels;
    using Alpha.MemberInfo.Site.Tests.Extensions;
    using Alpha.Web.Helpers;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class ReachedThresholdHandlerTests
    {
        private readonly MyWatchlistAccountsService watchlistAccountsService;
        private readonly ReachedThresholdService reachedThresholdService;
        private readonly ReachedThresholdHandler handler;
        private readonly BrowserHelper browserHelper;
        private readonly IUser user;

        public ReachedThresholdHandlerTests()
        {
            watchlistAccountsService = Substitute.For<MyWatchlistAccountsService>();
            reachedThresholdService = Substitute.For<ReachedThresholdService>();
            browserHelper = Substitute.For<BrowserHelper>();

            user = Substitute.For<IUser>();
            user.Id.Returns(1176086);
            user.Name.Returns("agent01");
            user.LevelId.Returns((byte)1);
            user.Language.Returns("en-US");

            handler = new ReachedThresholdHandler(reachedThresholdService, watchlistAccountsService, browserHelper);
        }

        [Fact]
        private ReachlistAccountReport CreateReachlistAccountReport()
           => new ReachlistAccountReport(
                  new List<ReachlistAccountModel>()
                  {
                        new ReachlistAccountModel(1, 1234, 0, "agent0101", new DateTime(), new DateTime(), "Global", "Open", 1000, 1000, 2000, 1, 0, 0, 1, 1, true),
                        new ReachlistAccountModel(2, 2345, 0, "agent0102", new DateTime(), new DateTime(), "Personal", "Open", 1000, 1000, 2000, 1, 0, 0, 1, 1, false),
                  },
              2);

        [Fact]
        public async Task HandleReachedThresholdQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var reachedThresholdMessage = new ReachedThresholdQuery(user);
            var reachlistAccountsMessage = new ReachlistAccountsQuery(user);

            var reachlistAccountReport = CreateReachlistAccountReport();
            var globalThreshold = new GlobalThresholdModel(1000, 2000);

            reachedThresholdService.GetReachlistAccounts(Arg.Any<SelectReachListParam>()).Returns(Task.FromResult(reachlistAccountReport));
            watchlistAccountsService.GetGlobalThreshold(Arg.Any<SelectGlobalThresholdParam>()).Returns(Task.FromResult(globalThreshold));

            var expectedViewModel = new ReachedThresholdViewModel(
                reachedThresholdMessage,
                globalThreshold,
                browserHelper.IsMobile(isOnlyTablet: false),
                new ReachedThresholdPagingViewModel(reachlistAccountReport, reachlistAccountsMessage));

            // Act
            var actualViewModel = await handler.Handle(reachedThresholdMessage);

            // Assert
            AssertExtension.Equal(expectedViewModel.ReachedThresholdPagingViewModel, actualViewModel.ReachedThresholdPagingViewModel);
            AssertExtension.Equal(expectedViewModel.GlobalThresholdModel, actualViewModel.GlobalThresholdModel);
            AssertExtension.Equal(expectedViewModel.StatusOptions, actualViewModel.StatusOptions);
            AssertExtension.Equal(expectedViewModel.LimitTypeOptions, actualViewModel.LimitTypeOptions);
            Assert.Equal(expectedViewModel.SearchAccountId, actualViewModel.SearchAccountId);
            Assert.Equal(expectedViewModel.IsMobile, actualViewModel.IsMobile);
            Assert.Equal(expectedViewModel.IsShowLastLoginColumn, actualViewModel.IsShowLastLoginColumn);
            Assert.Equal(expectedViewModel.CustLevel, actualViewModel.CustLevel);
        }

        [Fact]
        public async Task HandleWatchlistAccountsQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ReachlistAccountsQuery(user);
            var reachlistAccounts = CreateReachlistAccountReport();
            reachedThresholdService.GetReachlistAccounts(Arg.Any<SelectReachListParam>()).Returns(Task.FromResult(reachlistAccounts));
            var expectedViewModel = new ReachedThresholdPagingViewModel(reachlistAccounts, message);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public async Task HandleReachHistoryQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ReachHistoryQuery(user);
            IEnumerable<ReachHistoryModel> reachHistories = new List<ReachHistoryModel>()
            {
                new ReachHistoryModel(DateTime.Today, "Global", 1000, 1000, 2000, 1),
                new ReachHistoryModel(DateTime.Today, "Global", 2000, 3000, 4000, 2),
            };
            reachedThresholdService.GetReachDetail(Arg.Any<SelectReachDetailParam>()).Returns(Task.FromResult(reachHistories));

            var expectedViewModel = new ReachHistoryViewModel(message, reachHistories);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }
    }
}

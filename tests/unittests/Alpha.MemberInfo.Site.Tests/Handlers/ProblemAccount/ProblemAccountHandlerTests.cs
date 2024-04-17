using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Constants;
using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
using Alpha.MemberInfo.Params.ProblemAccount;
using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
using Alpha.Web.Helpers;

namespace Alpha.MemberInfo.Site.Tests.Handlers.ProblemAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Services;
    using MemberInfo.Models;
    using MemberInfo.Models.ProblemAccount;
    using NSubstitute;
    using Site.Handlers;
    using Site.Requests.ProblemAccount;
    using Xunit;

    [Trait("Category", "ProblemAccount")]
    public class ProblemAccountHandlerTests
    {
        private readonly IUser user;
        private readonly IProblemAccountService problemAccountService;
        private readonly MyWatchlistAccountsService myWatchlistAccountsService;
        private readonly BrowserHelper browserHelper;

        private readonly ProblemAccountHandler handler;
        public ProblemAccountHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)4);
            user.CanUpdateAccount.Returns(true);
            user.IsAllowedTotalBalanceTransfer.Returns(true);
            user.CanTransfer.Returns(true);
            user.CanViewBetLists.Returns(true);
            user.LimitedDateToShowInfo.Returns(DateTime.MinValue);

            problemAccountService = Substitute.For<IProblemAccountService>();
            myWatchlistAccountsService = Substitute.For<MyWatchlistAccountsService>();
            browserHelper = Substitute.For<BrowserHelper>();
            handler = new ProblemAccountHandler(problemAccountService, myWatchlistAccountsService, browserHelper);
        }

        [Fact]
        public void HandlerUpdateProblemAccountRequest_SuperLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new UpdateProblemAccountRequest(user);
            message.UserNames = "member01,member02,member03";
            message.RoleId = 4;
            message.Ids = "1,2,3";

            // Act
            var actual = handler.Handle(message);

            // Assert
            Assert.True(!actual.DisplayShowAllAccountOption
                        && actual.TopDownlineUsernames.Count() == 3
                        && actual.SelectedDownlineUsernames.Count() == 3
                        && !actual.MoreDownlineUsernames.Any()
                        && actual.TotalAllAccount == 3

                        && actual.Role == UserLevel.Super
                        && actual.Ids == message.Ids
            );
        }

        [Fact]
        public void HandlerUpdateProblemAccountCommand_AgentLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new UpdateProblemAccountCommand(user);

            problemAccountService.UpdateReviewStatus(Arg.Any<UpdateProblemAccountParam>()).Returns(new Result()
            {
                Code = 0,
                Message = Resources.UpdateSuccessfully
            });
            // Act
            var actual = handler.Handle(message);

            // Assert
            Assert.True(
                actual.IsSuccessful
                && actual.IsOk
                && actual.Message == Resources.UpdateSuccessfully
                && !actual.IsNotOk
                && !actual.IsFailed);
        }


        [Fact]
        public void HandlerIndexRequest_AgentLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new IndexRequest(user);
            browserHelper.IsMobile(false).Returns(false);
            var expected = new ProblemAccountReportViewModel(message, new List<ProblemAccountModel>(), browserHelper.IsMobile(false));
            // Act

            var actual = handler.Handle(message);

            // Assert
            Assert.True(
                !actual.IsMobileView
                && actual.ReviewedStatusSelectionViewModel.UserSelectedReviewedStatusId == ReviewStatusConstant.All
                && actual.DateRangePickerViewModel.MinDate == new DateTime(2020, 1, 1)
                && actual.DateRangePickerViewModel.MaxDate.Date == DateTime.Now.Date
                && !actual.Models.Any()
                && actual.CanUpdateAccount
                && actual.TotalColumnAgent == 9
                && actual.TotalColumnMember == 10
            );
        }

        [Fact]
        public void HandlerGetProblemAccountRequest_AgentLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new GetProblemAccountRequest(user);

            var expected = new List<ProblemAccountModel>()
            {
                new ProblemAccountModel()
                {
                    Status = 1,
                    CustId = 1,
                    IsReviewed = false,
                },
                new ProblemAccountModel()
                {
                    Status = 1,
                    CustId = 2,
                    IsReviewed = false,
                },
                new ProblemAccountModel()
                {
                    Status = 1,
                    CustId = 3,
                    IsReviewed = true,
                }
            };
            problemAccountService.Get(Arg.Any<GetProblemAccountParam>()).Returns(expected.AsEnumerable());
            // Act

            var actual = handler.Handle(message);

            // Assert
            Assert.True(actual.Count() == 3);
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using System.Web.Mvc;
    using Alpha.MemberInfo.Site.Requests.ProblemAccount;
    using Alpha.MemberInfo.Site.ViewModels;
    using Fixtures;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using Site.Controllers;
    using Site.Requests.MemberRequest;
    using Xunit;

    [Trait("Category", "ProblemAccount")]
    [Collection("UserCollection")]
    public class ProblemAccountControllerTests
    {
        private readonly IMediator mediator;
        private readonly ProblemAccountController controller;
        private readonly IUser user;
        private const string InvalidExcelFileName = "Invalid";
        protected const string InvalidPageViewPath = "~/Views/Shared/InvalidPage.cshtml";

        public ProblemAccountControllerTests()
        {
            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.Name.Returns("member01");
            user.Language.Returns("en");
            user.LevelId.Returns((byte)4);
            user.CanUpdateAccount.Returns(true);
            user.IsAllowedTotalBalanceTransfer.Returns(true);
            user.CanTransfer.Returns(true);
            user.CanViewBetLists.Returns(true);
            user.LimitedDateToShowInfo.Returns(DateTime.MinValue);
            mediator = Substitute.For<IMediator>();
            controller = new ProblemAccountController(mediator);

        }

        [Fact]
        public void Index_QueryIsValid_CallsMediatorIndexRequest()
        {
            // Arrange
            var message = new IndexRequest(user);

            // Act
            controller.Index(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Index_QueryIsNotValid_ReturnInvalidPageView()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new IndexRequest(user);

            // Act
            var actualView = controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal(InvalidPageViewPath, actualView.ViewName);
        }

        [Fact]
        public async Task ConfirmAddToWatchlist_QueryIsValid_CallsMediatorIndexRequest()
        {
            // Arrange
            var message = new ConfirmAddToWatchlistQuery(user);
            message.CustIds = "1,2,3";
            // Act
            await controller.ConfirmAddToWatchlist(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task ConfirmAddToWatchlist_QueryIsNotValid_ReturnInvalidPageView()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new ConfirmAddToWatchlistQuery(user);

            // Act
            var actualView = await controller.ConfirmAddToWatchlist(message) as ViewResult;

            // Assert
            Assert.Equal(InvalidPageViewPath, actualView.ViewName);
        }

        [Fact]
        public void Update_QueryIsValid_CallsMediatorIndexRequest()
        {
            // Arrange
            var message = new UpdateProblemAccountRequest(user);

            // Act
            controller.Update(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Update_QueryIsNotValid_ReturnInvalidPageView()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new UpdateProblemAccountRequest(user);

            // Act
            var actualView = controller.Update(message) as ViewResult;

            // Assert
            Assert.Equal(InvalidPageViewPath, actualView.ViewName);
        }

        [Fact]
        public void UpdateCommand_QueryIsValid_CallsMediatorIndexRequest()
        {
            // Arrange
            var message = new UpdateProblemAccountCommand(user);

            // Act
            controller.UpdateCommand(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void UpdateCommand_QueryIsNotValid_ReturnInvalidPageView()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new UpdateProblemAccountCommand(user);

            // Act
            var actualView = controller.UpdateCommand(message);

            // Assert
            mediator.DidNotReceive().Send(message);
        }

        [Fact]
        public void GetProblemAccounts_QueryIsValid_AlwaysCallsMediatorGetProblemAccountRequest()
        {
            // Arrange
            var message = new GetProblemAccountRequest(user);

            // Act
            controller.GetProblemAccounts(message); ;

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void GetProblemAccounts_QueryIsNotValid_DidNotCallsMediatorGetProblemAccountRequest()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new GetProblemAccountRequest(user);

            // Act
            controller.GetProblemAccounts(message); ;

            // Assert
            mediator.DidNotReceive().Send(message);
        }

        [Fact]
        public void Excel_QueryIsValid_AlwaysCallsMediatorProblemAccountExcelQuery()
        {
            // Arrange

            var message = new ProblemAccountExcelQuery(user);
            mediator
               .Send(Arg.Any<ProblemAccountExcelQuery>())
               .Returns(new ExcelFileViewModel("Excel File", new byte[0]));

            // Act
            controller.Excel(message);

            // Assert
            mediator.Received().Send(Arg.Is(message));
        }


        [Fact]
        public void Excel_QueryNotIsValid_DidNotCallsMediatorProblemAccountExcelQuery()
        {
            // Arrange
            user.Id.Returns(0);

            var message = new ProblemAccountExcelQuery(user);

            // Act
            controller.Excel(message);

            // Assert
            mediator.DidNotReceive().Send(Arg.Is(message));
        }
    }
}
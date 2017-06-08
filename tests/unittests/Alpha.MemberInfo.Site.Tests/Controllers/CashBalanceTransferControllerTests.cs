using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Controllers;
using Alpha.MemberInfo.Site.Requests.Transfer;
using MediatR;
using NSubstitute;
using Sunplus.Agent.Model.Entities;
using Xunit;

namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    [Trait("Category", "Transfer")]
    public class CashBalanceTransferControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;
        private readonly TransferController controller;

        public CashBalanceTransferControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();
            controller = new TransferController(mediator, userSession);
        }

        [Fact]
        public async Task CashBalance_Always_CallsMediatorCashBalanceTransferQuery()
        {
            // Arrange
            var message = new CashBalanceTransferQuery { CustId = 1176083 };
            userSession.IsInternal.Returns(true);

            // Act
            await controller.CashBalance(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Theory]
        [InlineData(-1176083)]
        [InlineData(0)]
        public async Task CashBalance_QueryIsNotValid_ReturnInvalidPageView(int custId)
        {
            // Arrange
            var message = new CashBalanceTransferQuery { CustId = custId };

            // Act
            var actualView = await controller.CashBalance(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public async Task CashBalance_QueryIsAllDayBalTransfer_ReturnInvalidPageView()
        {
            // Arrange
            var message = new CashBalanceTransferQuery { CustId = 1176083 };
            userSession.IsInternal.Returns(false);

            // Act
            var actualView = await controller.CashBalance(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public async Task CashBalance_Always_CallsMediatorCashBalanceTransferCommand()
        {
            // Arrange
            var command = new CashBalanceTransferCommand { CustId = 1176083, Amount = 1101240.32M };

            userSession.IsInternal.Returns(true);
            userSession.CustId.Returns(1176070);
            userSession.SubAccId.Returns(0);
            userSession.UserLoginIp.Returns("127.0.0.1");

            // Act
            await controller.CashBalance(command);

            // Assert
            await mediator.Received().SendAsync(command);
        }

        [Theory]
        [InlineData(-1176083, 1101240.32)]
        [InlineData(0, 1101240.32)]
        [InlineData(1176083, 0)]
        public async Task CashBalance_CommandIsNotValid_ReturnBadRequestResult(int custId, decimal amount)
        {
            // Arrange
            var command = new CashBalanceTransferCommand { CustId = custId, Amount = amount };
            var expectedResult = Result.CreateErrorResult("Bad Request.", 400);

            // Act
            var actualResult = await controller.CashBalance(command) as JsonResult;

            // Assert
            var actualData = (Result)actualResult.Data;

            Assert.True(actualData.Code == expectedResult.Code &&
                actualData.Message == expectedResult.Message);
        }

        [Fact]
        public async Task CashBalance_CommandIsAllDayBalTransfer_ReturnBadRequestResult()
        {
            // Arrange
            var command = new CashBalanceTransferCommand { CustId = 1176083, Amount = 1101240.32M };
            var expectedResult = Result.CreateErrorResult("Bad Request.", 400);

            userSession.IsInternal.Returns(false);

            // Act
            var actualResult = await controller.CashBalance(command) as JsonResult;

            // Assert
            var actualData = (Result)actualResult.Data;

            Assert.True(actualData.Code == expectedResult.Code &&
                actualData.Message == expectedResult.Message);
        }
    }
}

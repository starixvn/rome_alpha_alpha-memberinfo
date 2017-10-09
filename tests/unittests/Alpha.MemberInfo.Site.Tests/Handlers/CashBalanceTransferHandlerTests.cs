using System.Threading.Tasks;
using Alpha.MemberInfo.AppResources;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Models.Transfer;
using Alpha.MemberInfo.Services;
using Alpha.MemberInfo.Site.Handlers;
using Alpha.MemberInfo.Site.Requests.Transfer;
using Alpha.MemberInfo.Site.Tests.Extensions;
using Alpha.MemberInfo.Site.ViewModels.Transfer;
using NSubstitute;
using Xunit;

namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    [Trait("Category", "Transfer")]
    public class CashBalanceTransferHandlerTests
    {
        private readonly IUser user;
        private readonly CashBalanceTransferService service;
        private readonly CashBalanceTransferHandler handler;

        public CashBalanceTransferHandlerTests()
        {
            user = Substitute.For<IUser>();
            service = Substitute.For<CashBalanceTransferService>();
            handler = new CashBalanceTransferHandler(service);
        }

        [Fact]
        public async Task HandlerCashBalanceTransferQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            int custId = 1176083;
            var message = new CashBalanceTransferQuery { CustId = custId };
            var cashBalanceTransfer = new CashBalanceTransfer(
                1176083,
                "master1",
                3,
                67599433.9247M,
                1101240.3283M,
                342552.00M,
                900000002.00M);

            var expectedViewModel = new CashBalanceTransferViewModel(
                cashBalanceTransfer.UplineCashBalance,
                cashBalanceTransfer.CustId,
                cashBalanceTransfer.UserName,
                Enumeration.FromValue<UserLevel>(cashBalanceTransfer.RoleId),
                cashBalanceTransfer.CashBalance,
                cashBalanceTransfer.Outstanding,
                cashBalanceTransfer.GivenCredit);

            service.GetCashBalanceTransferAsync(custId).Returns(cashBalanceTransfer);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public async Task HandlerCashBalanceTransferCommand_Always_ReturnsSuccessfulResult()
        {
            // Arrange
            var expectedResult = new Result { Message = Transfer.TransferSuccessfully };

            user.Id.Returns(1176070);
            user.SubAccId.Returns(0);
            user.IP.Returns("127.0.0.1");
            user.LanguageKey.Returns("E");

            var command = new CashBalanceTransferCommand(user)
            {
                CustId = 1176083,
                Amount = 1101240.32M
            };

            service.AddCashBalanceTransferAsync(
                command.UplineId,
                command.CustId,
                command.Amount,
                command.UplineId,
                command.UplineName,
                command.SubAccId,
                command.IP,
                command.LanguageKey).Returns(expectedResult);

            // Act
            var actualResult = await handler.Handle(command);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }
    }
}

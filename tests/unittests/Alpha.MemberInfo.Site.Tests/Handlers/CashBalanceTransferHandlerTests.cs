//using System.Threading.Tasks;
//using Alpha.MemberInfo.Enumerations;
//using Alpha.MemberInfo.Models;
//using Alpha.MemberInfo.Models.Transfer;
//using Alpha.MemberInfo.Services;
//using Alpha.MemberInfo.Site.Handlers;
//using Alpha.MemberInfo.Site.Requests.Transfer;
//using Alpha.MemberInfo.Site.Tests.Extensions;
//using Alpha.MemberInfo.Site.ViewModels.Transfer;
//using NSubstitute;
//using Xunit;

//namespace Alpha.MemberInfo.Site.Tests.Handlers
//{
//    [Trait("Category", "Transfer")]
//    public class CashBalanceTransferHandlerTests
//    {
//        private readonly CashBalanceTransferService service;
//        private readonly CashBalanceTransferHandler handler;

//        public CashBalanceTransferHandlerTests()
//        {
//            service = Substitute.For<CashBalanceTransferService>();
//            handler = new CashBalanceTransferHandler(service);
//        }

//        [Fact]
//        public async Task HandlerCashBalanceTransferQuery_Always_ReturnsCorrectViewModel()
//        {
//            // Arrange
//            int custId = 1176083;
//            var message = new CashBalanceTransferQuery { CustId = custId };
//            var cashBalanceTransfer = new CashBalanceTransfer
//            {
//                CustId = 1176083,
//                UserName = "master1",
//                RoleId = 3,
//                UplineCashBalance = 67599433.9247M,
//                CashBalance = 1101240.3283M,
//                Outstanding = 342552.00M,
//                GivenCredit = 900000002.00M
//            };

//            var expectedViewModel = new CashBalanceTransferViewModel(
//                cashBalanceTransfer.UplineCashBalance,
//                cashBalanceTransfer.CustId,
//                cashBalanceTransfer.UserName,
//                Enumeration.FromValue<UserLevel>(cashBalanceTransfer.RoleId),
//                cashBalanceTransfer.CashBalance,
//                cashBalanceTransfer.Outstanding,
//                cashBalanceTransfer.GivenCredit);

//            service.GetCashBalanceTransferAsync(custId).Returns(cashBalanceTransfer);

//            // Act
//            var actualViewModel = await handler.Handle(message);

//            // Assert
//            AssertExtension.Equal(expectedViewModel, actualViewModel);
//        }

//        [Fact]
//        public async Task HandlerCashBalanceTransferCommand_Always_ReturnsSuccessfulResult()
//        {
//            // Arrange
//            var expectedResult = new Result { Message = AppResources.Transfer.TransferSuccessfully };

//            var command = new CashBalanceTransferCommand
//            {
//                UplineId = 1176070,
//                CustId = 1176083,
//                Amount = 1101240.32M,
//                SuperIdsTransferTotal = string.Empty,
//                SubAccId = 0,
//                IP = "127.0.0.1",
//                LanguageKey = "E"
//            };

//            service.UpdateCashBalanceTransferAsync(
//                command.UplineId,
//                command.CustId,
//                command.Amount,
//                command.SuperIdsTransferTotal,
//                command.SubAccId,
//                command.IP,
//                command.LanguageKey).Returns(expectedResult);

//            // Act
//            var actualResult = await handler.Handle(command);

//            // Assert
//            AssertExtension.Equal(expectedResult, actualResult);
//        }
//    }
//}

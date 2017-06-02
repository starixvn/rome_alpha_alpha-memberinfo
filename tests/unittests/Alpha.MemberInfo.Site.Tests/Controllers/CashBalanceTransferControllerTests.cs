//using System.Web.Mvc;
//using Alpha.MemberInfo.Site.Controllers;
//using Alpha.MemberInfo.Site.Requests.Transfer;
//using MediatR;
//using NSubstitute;
//using Xunit;

//namespace Alpha.MemberInfo.Site.Tests.Controllers
//{
//    public class CashBalanceTransferControllerTests
//    {
//        private readonly IMediator mediator;
//        private readonly CashBalanceTransferController controller;

//        public CashBalanceTransferControllerTests()
//        {
//            mediator = Substitute.For<IMediator>();
//            controller = new CashBalanceTransferController(mediator);
//        }

//        [Fact]
//        public void Index_Always_CallsMediatorCashBalanceTransferQuery()
//        {
//            // Arrange
//            var message = new CashBalanceTransferQuery { CustId = 23003440, CustName = "SupertestTE010101" };

//            // Act
//            controller.Index(message);

//            // Assert
//            mediator.Received().Send(message);
//        }

//        [Fact]
//        public void Index_QueryIsNotValid_ReturnInvalidPageView()
//        {
//            // Arrange
//            var message = new CashBalanceTransferQuery { CustId = -23003440, CustName = "SupertestTE010101" };

//            // Act
//            var actualView = controller.Index(message) as ViewResult;

//            // Assert
//            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
//        }
//    }
//}

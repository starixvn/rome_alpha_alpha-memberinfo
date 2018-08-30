namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Models;
    using MemberInfo.Models.WinLossLimit;
    using NSubstitute;
    using Params;
    using Requests.WinLossLimit;
    using Services.WinLossLimit;
    using Site.Handlers;
    using Site.ViewModels.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class WinLossLimitHandlerTests
    {
        private readonly WinLossLimitService service;
        private readonly WinLossLimitHandler handler;
        private readonly CompareLogic compareLogic;

        public WinLossLimitHandlerTests()
        {
            service = Substitute.For<WinLossLimitService>();
            handler = new WinLossLimitHandler(service);

            compareLogic = new CompareLogic();

            service.GetWinLossLimitProducts(Arg.Any<int>()).Returns(Enumerable.Empty<WinLossLimitProductModel>());
            service.GetMasterList(Arg.Any<int>()).Returns(Enumerable.Empty<CustomerInfo>());
            service.GetAgentList(Arg.Any<int>()).Returns(Enumerable.Empty<CustomerInfo>());
        }

        [Fact]
        public void HandlerWinLossLimitQuery_SuperLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            var user = Substitute.For<IUser>();
            user.LevelId.Returns((byte)4);

            var message = new WinLossLimitQuery(user)
            {
                UserSelectedProductId = 1
            };

            var param = new WinLossLimitParam(
                1,
                message.CustId,
                0,
                0,
                message.Username,
                message.PlayerId,
                0,
                0,
                message.PageSize,
                message.PageIndex);
            var expectedViewModel = new WinLossLimitViewModel(
                message,
                new List<WinLossLimitProductModel>(),
                Enumerable.Empty<CustomerInfo>(),
                Enumerable.Empty<CustomerInfo>(),
                new WinLossLimitPage(),
                param);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}

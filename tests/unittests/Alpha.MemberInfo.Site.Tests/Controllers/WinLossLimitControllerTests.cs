namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using Fixtures;
    using MediatR;
    using NSubstitute;
    using Requests.WinLossLimit;
    using Site.Controllers;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    [Collection("UserCollection")]
    public class WinLossLimitControllerTests
    {
        private readonly IMediator mediator;
        private readonly UserFixture userFixture;
        private readonly WinLossLimitController controller;

        public WinLossLimitControllerTests(UserFixture userFixture)
        {
            mediator = Substitute.For<IMediator>();
            controller = new WinLossLimitController(mediator);
            this.userFixture = userFixture;
        }

        [Fact]
        public void Index_Always_CallsMediatorProductQuery()
        {
            // Arrange
            var message = new WinLossLimitQuery(userFixture.User);

            // Act
            controller.Index(message);

            // Assert
            mediator.Received().Send(message);
        }
    }
}

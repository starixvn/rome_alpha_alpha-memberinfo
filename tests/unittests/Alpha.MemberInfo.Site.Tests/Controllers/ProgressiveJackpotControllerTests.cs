namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using System.Web.Mvc;
    using Fixtures;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using Site.Controllers;
    using Site.Requests.ProgressiveJackpot;
    using Xunit;

    [Collection("UserCollection")]
    [Trait("Category", "ProgressiveJackpot")]
    public class ProgressiveJackpotControllerTests
    {
        private readonly IMediator mediator;
        private readonly UserFixture userFixture;
        private readonly ProgressiveJackpotController controller;

        public ProgressiveJackpotControllerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            mediator = Substitute.For<IMediator>();
            controller = new ProgressiveJackpotController(mediator, userFixture.User);
        }

        [Fact]
        public void Index_Always_CallsMediatorSendProgressiveJackpotQuery()
        {
            // Arrange
            var message = new ProgressiveJacpotQuery(userFixture.User);

            // Act
            controller.Index(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Index_QueryIsNotValid_ReturnInvalidPageView()
        {
            // Arrange
            var user = Substitute.For<IUser>();
            user.Id.Returns(0);
            var message = new ProgressiveJacpotQuery(user);

            // Act
            var actualView = controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }
    }
}
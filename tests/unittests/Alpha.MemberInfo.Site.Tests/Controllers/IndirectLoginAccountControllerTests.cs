namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Controllers;
    using Alpha.MemberInfo.Site.Requests.IndirectLoginAccount;
    using MediatR;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class IndirectLoginAccountControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly IndirectLoginAccountController controller;

        public IndirectLoginAccountControllerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.IP.Returns("127.0.0.1");
            user.Language.Returns("en-US");
            user.Name.Returns("master1");
            user.LevelId.Returns((byte)3);
            user.IsAllowedIndirectLogin.Returns(true);

            mediator = Substitute.For<IMediator>();
            controller = new IndirectLoginAccountController(mediator);
        }

        [Fact]
        public async Task Index_ValidQuery_CallsMediatorManageAccountQuery()
        {
            // Arrange
            var message = new ManageAccountQuery(user);

            // Act
            await controller.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task Index_InvalidQuery_ReturnsInvalidPage()
        {
            // Arrange
            user.Id.Returns(0);
            var message = new ManageAccountQuery(user);

            // Act
            var actualView = await controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public void NewAccount_Always_CallsMediatorNewAccountQuery()
        {
            // Arrange
            var message = new NewAccountQuery();

            // Act
            controller.NewAccount(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public async Task NewAccount_ValidCommand_CallsMediatorNewAccountCommand()
        {
            // Arrange
            var message = new NewAccountCommand(user)
            {
                Password = "Fg0Yjs4OUKceI1P9SSxOrw=="
            };

            // Act
            await controller.NewAccount(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task NewAccount_InvalidCommand_ReturnsNotAuthorizedMessage()
        {
            // Arrange
            var message = new NewAccountCommand(user);

            // Act
            var actualResult = await controller.NewAccount(message) as JsonResult;
            var actualData = (Result)actualResult.Data;

            // Assert
            Assert.Equal(Lang.NotAuthorized, actualData.Message);
        }

        [Fact]
        public void UpdatePassword_ValidQuery_CallsMediatorUpdatePasswordQuery()
        {
            // Arrange
            var message = new UpdatePasswordQuery(user);

            // Act
            controller.UpdatePassword(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void UpdatePassword_InvalidQuery_ReturnsInvalidPage()
        {
            // Arrange
            user.Name.Returns(string.Empty);
            var message = new UpdatePasswordQuery(user);

            // Act
            var actualView = controller.UpdatePassword(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public async Task UpdatePassword_ValidCommand_CallsMediatorUpdatePasswordCommand()
        {
            // Arrange
            var message = new UpdatePasswordCommand(user)
            {
                Password = "Fg0Yjs4OUKceI1P9SSxOrw=="
            };

            // Act
            await controller.UpdatePassword(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task UpdatePassword_InvalidCommand_ReturnsNotAuthorizedMessage()
        {
            // Arrange
            var message = new UpdatePasswordCommand(user);

            // Act
            var actualResult = await controller.UpdatePassword(message) as JsonResult;
            var actualData = (Result)actualResult.Data;

            // Assert
            Assert.Equal(Lang.NotAuthorized, actualData.Message);
        }

        [Fact]
        public async Task Disconnect_ValidCommand_CallsMediatorDisconnectCommand()
        {
            // Arrange
            var message = new DisconnectCommand(user);

            // Act
            await controller.Disconnect(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task Disconnect_InvalidCommand_ReturnsNotAuthorizedMessage()
        {
            // Arrange
            user.Language.Returns(string.Empty);
            var message = new DisconnectCommand(user);

            // Act
            var actualResult = await controller.Disconnect(message) as JsonResult;
            var actualData = (Result)actualResult.Data;

            // Assert
            Assert.Equal(Lang.NotAuthorized, actualData.Message);
        }
    }
}

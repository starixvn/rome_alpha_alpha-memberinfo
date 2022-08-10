namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.MyWatchlistAccounts
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using MediatR;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class MyWatchlistAccountsControllerTests
    {
        private readonly MyWatchlistAccountsController controller;
        private readonly IMediator mediator;
        private readonly IUser user;

        private const int DefaultErrorCode = 1;
        private const string NotAuthorizedErrorMessage = "You are not authorized on this function.";

        public MyWatchlistAccountsControllerTests()
        {
            mediator = Substitute.For<IMediator>();

            user = Substitute.For<IUser>();
            user.Id.Returns(1176086);
            user.Name.Returns("agent01");
            user.LevelId.Returns((byte)1);
            user.Language.Returns("en-US");

            controller = new MyWatchlistAccountsController(mediator);
        }

        [Fact]
        public async Task Index_IsValid_CallsMediatorMyWatchlistAccountsQuery()
        {
            // Arrange
            var message = new MyWatchlistAccountsQuery(user);

            // Act
            await controller.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task ManageWatchlistAccounts_IsValid_CallsMediatorManageWatchlistAccountsQuery()
        {
            // Arrange
            var message = new ManageWatchlistAccountsQuery(user);

            // Act
            await controller.ManageWatchlistAccounts(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task ManageWatchlistAccounts_IsValid_CallsMediatorManageGlobalThresholdQuery()
        {
            // Arrange
            var message = new ManageGlobalThresholdQuery(user);

            // Act
            await controller.ManageGlobalThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task ManageWatchlistAccounts_IsValid_CallsMediatorManagePersonalThresholdPresetQuery()
        {
            // Arrange
            var message = new ManagePersonalThresholdPresetQuery(user);

            // Act
            await controller.ManagePersonalThresholdPreset(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task GetWatchlistAccounts_IsValid_CallsMediatorWatchlistAccountsQuery()
        {
            // Arrange
            var message = new WatchlistAccountsQuery(user);

            // Act
            await controller.GetWatchlistAccounts(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task AddPersonalThreshold_IsValid_CallsMediatorAddPersonalThresholdCommand()
        {
            // Arrange
            var message = new AddPersonalThresholdCommand(user);

            // Act
            await controller.AddPersonalThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task AddPersonalThreshold_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new AddPersonalThresholdCommand(user);

            // Act
            var actualData = await controller.AddPersonalThreshold(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public async Task UpdateMultiplePersonalThreshold_IsValid_CallsMediatorUpdateMultiplePersonalThresholdCommand()
        {
            // Arrange
            var message = new UpdateMultiplePersonalThresholdCommand(user);

            // Act
            await controller.UpdateMultiplePersonalThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task UpdateMultiplePersonalThreshold_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new UpdateMultiplePersonalThresholdCommand(user);

            // Act
            var actualData = await controller.UpdateMultiplePersonalThreshold(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public async Task DeleteMultiplePersonalThreshold_IsValid_CallsMediatorDeleteMultiplePersonalThresholdCommand()
        {
            // Arrange
            var message = new DeleteMultiplePersonalThresholdCommand(user);

            // Act
            await controller.DeleteMultiplePersonalThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task DeleteMultiplePersonalThreshold_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new DeleteMultiplePersonalThresholdCommand(user);

            // Act
            var actualData = await controller.DeleteMultiplePersonalThreshold(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public async Task UpdateGlobalThreshold_IsValid_CallsMediatorUpdateGlobalThresholdCommand()
        {
            // Arrange
            var message = new UpdateGlobalThresholdCommand(user);

            // Act
            await controller.UpdateGlobalThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task UpdateGlobalThreshold_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new UpdateGlobalThresholdCommand(user);

            // Act
            var actualData = await controller.UpdateGlobalThreshold(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public async Task GetPersonalPresets_IsValid_CallsMediatorPersonalPresetQuery()
        {
            // Arrange
            var message = new PersonalPresetQuery(user);

            // Act
            await controller.GetPersonalPresets(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task AddMultiplePersonalPreset_IsValid_CallsMediatorAddMultiplePersonalPresetCommand()
        {
            // Arrange
            var message = new AddMultiplePersonalPresetCommand(user);

            // Act
            await controller.AddMultiplePersonalPreset(message);

            // Arrange
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task AddMultiplePersonalPreset_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new AddMultiplePersonalPresetCommand(user);

            // Act
            var actualData = await controller.AddMultiplePersonalPreset(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }


        [Fact]
        public async Task UpdatePersonalPreset_IsValid_CallsMediatorUpdatePersonalPresetCommand()
        {
            // Arrange
            var message = new UpdatePersonalPresetCommand(user);

            // Act
            await controller.UpdatePersonalPreset(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task UpdatePersonalPreset_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new UpdatePersonalPresetCommand(user);

            // Act
            var actualData = await controller.UpdatePersonalPreset(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public async Task DeletePersonalPreset_IsValid_CallsMediatorDeletePersonalPresetCommand()
        {
            // Arrange
            var message = new DeletePersonalPresetCommand(user);

            // Act
            await controller.DeletePersonalPreset(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task DeletePersonalPreset_IsInternal_ReturnsNotAuthorizedErrorResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var message = new DeletePersonalPresetCommand(user);

            // Act
            var actualData = await controller.DeletePersonalPreset(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            await mediator.DidNotReceive().SendAsync(message);
            Assert.Equal(DefaultErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }
    }
}

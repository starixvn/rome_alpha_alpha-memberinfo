using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.Features.CopyAccount;
using Alpha.MemberInfo.Site.Features.CopyAccount.Request;
using MediatR;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;

namespace Alpha.MemberInfo.Site.Tests.Features.CopyAccount
{
    [Trait("Category", "QRCodeMemberSignIn")]
    public class CopyAccountControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUser user;
        private readonly CopyAccountController copyAccountController;

        private const int BadRequestErrorCode = 999;
        private const string NotAuthorizedErrorMessage = "You are not authorized on this function.";

        public CopyAccountControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)2);
            user.Name.Returns("UserTest");
            user.Language.Returns("En");
            user.IsInternal.Returns(false);

            copyAccountController = new CopyAccountController(mediator);
        }

        [Fact]
        public async Task Index_Always_CallsMediatorCopyAccountQuery()
        {
            // Arrange
            var message = new CopyAccountQuery(user);

            // Act
            await copyAccountController.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task GetCredit_Always_CallsMediatorCreditQuery()
        {
            // Arrange
            var message = new CreditQuery(user);

            // Act
            await copyAccountController.GetCredit(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task CheckUsername_Always_CallsMediatorCheckUsernameQuery()
        {
            // Arrange
            var message = new CheckUsernameQuery(user);

            // Act
            await copyAccountController.CheckUsername(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task CheckExistedCustomer_Always_CallsMediatorCheckExistedCustomerQuery()
        {
            // Arrange
            var message = new CheckExistedCustomerQuery(user);

            // Act
            await copyAccountController.CheckExistedCustomer(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task Index_CopyAccountCommandIsInvalid_ReturnsNotAuthorizedResult()
        {
            // Arrange
            user.IsInternal.Returns(true);
            var command = new CopyAccountCommand(user);

            // Act
            var actualResult = await copyAccountController.Index(command) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualResult.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }
    }
}
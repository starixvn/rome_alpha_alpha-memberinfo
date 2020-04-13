namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using System;
    using System.Web.Mvc;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class QRCodeMemberSignInControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUser user;
        private readonly QRCodeMemberSignInController controller;

        public QRCodeMemberSignInControllerTests()
        {
            mediator = Substitute.For<IMediator>();

            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)2);
            user.Name.Returns("UserTest");
            user.Language.Returns("En");

            controller = new QRCodeMemberSignInController(mediator);
        }

        [Fact]
        public void Index_Always_CallsMediatorQRCodeMemberSignInRequest()
        {
            // Arrange
            var message = new QRCodeMemberSignInRequest(user);

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
            user.LevelId.Returns((byte)3);
            var message = new QRCodeMemberSignInRequest(user);

            // Act
            var actualView = controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }


        [Fact]
        public void CreateAccount_Success_RedirectToShowQRCode()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin= 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            mediator.Send(Arg.Any<CreateAccountByQRCodeRequest>()).Returns(
                new ResultCreateMemberQRCode
                {
                    Success = true,
                    CustId = 1,
                    CustIdCopyFrom = 2,
                    ErrCode = 0,
                    ErrMsg = string.Empty
                } );

            // Act
            var action = (RedirectToRouteResult) controller.CreateAccount(message);

            // Assert
            Assert.Equal("ShowQRCode", action.RouteValues["action"]);
            Assert.Equal(action.RouteValues["SearchAccountName"], "UserTestAAA");
            Assert.Equal(action.RouteValues["SearchAccountId"], 1);
            Assert.Equal(action.RouteValues["FirstName"], "James");
            Assert.Equal(action.RouteValues["Credit"], (decimal)100);
            Assert.Equal(action.RouteValues["CreditMin"], (decimal)10);
            Assert.Equal(action.RouteValues["CreditMax"], (decimal)1000);
            Assert.Equal(action.RouteValues["MemberNameCopyFrom"], "User");
            Assert.Equal(action.RouteValues["ShowPageTitle"], true);
            Assert.Equal(action.RouteValues["CreateMode"], true);
            Assert.Equal(action.RouteValues["MemberIdCopyFrom"], 2);
        }

        [Fact]
        public void CreateAccount_Failed_RedirectToIndex()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            var result = new ResultCreateMemberQRCode
            {
                Success = false,
                CustId = 1,
                CustIdCopyFrom = 2,
                ErrCode = 100,
                ErrMsg = "User is not availble"
            };

            mediator.Send(Arg.Any<CreateAccountByQRCodeRequest>()).Returns(result);

            // Act
            var action = (RedirectToRouteResult)controller.CreateAccount(message);

            // Assert
            Assert.Equal("Index", action.RouteValues["action"]);
            Assert.Equal(action.RouteValues["MemberName"], "User");
            Assert.Equal(action.RouteValues["ErrorMessage"], result.ErrMsg);
        }

        [Theory]
        [InlineData("", 10, 100, 1000)]
        [InlineData(null, 10, 100, 1000)]
        [InlineData("UserCopy", 10, 0, 1000)]
        [InlineData("", 10, 100, 99)]
        public void CreateAccount_InvalidQuery_RedirectToIndex(
            string usernameCopyFrom,
            decimal creditMin,
            decimal credit,
            decimal creditMax)
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = usernameCopyFrom,
                Credit = credit,
                CreditMin = creditMin,
                CreditMax = creditMax,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            // Act
            var action = (RedirectToRouteResult)controller.CreateAccount(message);

            // Assert
            Assert.Equal("Index", action.RouteValues["action"]);
            Assert.Equal(action.RouteValues["ErrorMessage"], "Username is invalid.");
        }

        [Fact]
        public void CreateAccount_IsInvalidCaptcha_RedirectToIndex()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            controller.ModelState.AddModelError("QRCode", "Invalid Captcha.");

            // Act
            var action = (RedirectToRouteResult)controller.CreateAccount(message);

            // Assert
            Assert.Equal("Index", action.RouteValues["action"]);
            Assert.Equal(action.RouteValues["MemberName"], "User");
            Assert.Equal(action.RouteValues["ErrorMessage"], "Invalid Captcha.");
        }

        [Fact]
        public void GetUsernamesSuggestion_Always_CallsMediatorUserNameSuggestionQuery()
        {
            // Arrange
            var message = new UserNameSuggestionQuery(user)
            {
                SuggestionQuery = "AA"
            };

            // Act
            controller.GetUsernamesSuggestion(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void ShowQRCode_Always_CallsMediatorShowQRCodeRequest()
        {
            // Arrange
            var message = new ShowQRCodeRequest(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "user test",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000
            };

            // Act
            controller.ShowQRCode(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0, "user", 10, 100, 1000)]
        [InlineData(1, null, 10, 100, 1000)]
        [InlineData(1, "", 10, 100, 1000)]
        [InlineData(1, "user", 10, 0, 1000)]
        [InlineData(1, "user", 10, 100, 99)]
        public void ShowQRCode_QueryIsNotValid_ReturnInvalidPageView(
            int searchAccountId,
            string searchAccountName,
            decimal creditMin,
            decimal credit,
            decimal creditMax)
        {
            // Arrange
            var user = Substitute.For<IUser>();
            var message = new ShowQRCodeRequest(user)
            {
                SearchAccountId = searchAccountId,
                SearchAccountName = searchAccountName,
                Credit = credit,
                CreditMin = creditMin,
                CreditMax = creditMax
            };

            // Act
            var actualView = controller.ShowQRCode(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public void GenerateQrCode_Always_CallsMemberRequestsQuery()
        {
            // Arrange
            var message = new GenerateQRCodeRequest(user);

            // Act
            controller.GenerateQrCode(message);

            // Assert
            mediator.Received().Send(message);
        }
    }
}

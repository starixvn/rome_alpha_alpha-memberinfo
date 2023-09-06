namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using Sunplus.Agent.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class QRCodeMemberSignInControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;
        private readonly IUser user;
        private readonly QRCodeMemberSignInController controller;

        public QRCodeMemberSignInControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            userSession = Substitute.For<IUserSession>();

            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)2);
            user.Name.Returns("UserTest");
            user.Language.Returns("En");
            user.IsInternal.Returns(false);

            controller = new QRCodeMemberSignInController(mediator, userSession);
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
            var iuser = Substitute.For<IUser>();
            iuser.LevelId.Returns((byte)3);
            var message = new QRCodeMemberSignInRequest(iuser);

            // Act
            var actualView = controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }


        [Fact]
        public void CreateAccount_Success_ReturnCorrectDataWithRawPassword()
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
                Success = true,
                CustId = 1,
                CustIdCopyFrom = 2,
                ErrCode = 0,
                ErrMsg = string.Empty
            };

            mediator.SendAsync(Arg.Any<CreateAccountByQRCodeRequest>()).Returns(result);

            // Act
            var actualData = controller.CreateAccount(message).Result as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.ErrCode, data["ErrCode"]);
            Assert.Equal(result.ErrMsg, data["ErrMsg"]);
            Assert.Equal(result.CustIdCopyFrom, data["CustIdCopyFrom"]);
            Assert.Equal(result.RawPassword, data["RawPassword"]);
            Assert.Equal(result.CustId, data["SearchAccountId"]);
            Assert.Equal("UserTestAAA", data["SearchAccountName"]);
            Assert.Equal(message.FirstName, data["FirstName"]);
            Assert.Equal(message.Credit, data["Credit"]);
            Assert.Equal(message.CreditMin, data["CreditMin"]);
            Assert.Equal(message.CreditMax, data["CreditMax"]);
            Assert.Equal(message.ShowPageTitle, data["ShowPageTitle"]);
            Assert.True((bool)data["CreateMode"]);
        }

        [Fact]
        public void CreateAccount_Failed_ReturnErrorResult()
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

            mediator.SendAsync(Arg.Any<CreateAccountByQRCodeRequest>()).Returns(result);

            // Act
            var actualData = controller.CreateAccount(message).Result as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.ErrCode, data["ErrCode"]);
            Assert.Equal(result.ErrMsg, data["ErrMsg"]);
        }

        [Theory]
        [InlineData("", 10, 100, 1000)]
        [InlineData(null, 10, 100, 1000)]
        [InlineData("UserCopy", 10, 0, 1000)]
        [InlineData("", 10, 100, 99)]
        public void CreateAccount_InvalidQuery_ReturnErrorResult(
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
            var actualData = controller.CreateAccount(message).Result as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(998, data["ErrCode"]);
            Assert.Equal("This username is not available.", data["ErrMsg"]);
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
            var iuser = Substitute.For<IUser>();
            var message = new ShowQRCodeRequest(iuser)
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

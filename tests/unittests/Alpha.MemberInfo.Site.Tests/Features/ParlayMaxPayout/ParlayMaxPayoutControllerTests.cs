namespace Alpha.MemberInfo.Site.Tests.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests;
    using Alpha.MemberInfo.Site.Tests.Extensions;
    using MediatR;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUser user;
        private readonly ParlayMaxPayoutController controller;

        private const int BadRequestErrorCode = 999;
        private const string NotAuthorizedErrorMessage = "You are not authorized on this function.";

        public ParlayMaxPayoutControllerTests()
        {
            mediator = Substitute.For<IMediator>();

            user = Substitute.For<IUser>();
            user.Id.Returns(1176070);
            user.Name.Returns("supertest1");
            user.LevelId.Returns((byte)4);
            user.Language.Returns("En");
            user.CanUpdateAccount.Returns(true);
            user.IsInternal.Returns(false);

            controller = new ParlayMaxPayoutController(mediator);
        }

        [Fact]
        public void EditSingle_ValidQuery_CallsMediatorParlayMaxPayoutSingleSelectQuery()
        {
            // Arrange
            var message = new ParlayMaxPayoutSingleSelectQuery(user)
            {
                CustId = 14373487,
                CustName = "masterkz"
            };

            // Act
            controller.EditSingle(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(-14373487, "masterkz")]
        [InlineData(14373487, "")]
        [InlineData(-14373487, "")]
        public void EditSingle_InValidQuery_ReturnsInvalidPage(int custId, string custName)
        {
            // Arrange
            var message = new ParlayMaxPayoutSingleSelectQuery(user)
            {
                CustId = custId,
                CustName = custName
            };

            // Act

            var actualView = controller.EditSingle(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public void EditMultiple_ValidQuery_CallsMediatorParlayMaxPayoutMultipleSelectQuery()
        {
            // Arrange
            var message = new ParlayMaxPayoutMultipleSelectQuery(user)
            {
                CustIds = "14373487, 14373488, 14373489",
                CustNames = "masterkz, masterkz1, masterkz2"
            };

            // Action
            controller.EditMultiple(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData("14373487, 14373488, 14373489", "")]
        [InlineData("", "masterkz, masterkz1, masterkz2")]
        [InlineData("", "")]
        public void EditMultiple_InValidQuery_ReturnsInvalidPage(string custIds, string custNames)
        {
            // Arrange
            var message = new ParlayMaxPayoutMultipleSelectQuery(user)
            {
                CustIds = custIds,
                CustNames = custNames
            };

            // Action
            var actualView = controller.EditMultiple(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Theory]
        [InlineData("14373487", "")]
        [InlineData("", "<ROOT></ROOT>")]
        [InlineData("", "")]
        public void UpdateSingle_InValidQuery_ReturnsUnAuthorizedErrorResult(string custIdList, string maxPayoutLimit)
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = custIdList,
                MaxPayoutLimit = maxPayoutLimit
            };

            // Action
            var actualData = controller.UpdateSingle(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public void UpdateSingle_CannotUpdateAccountOrIsInternalOrBoth_ReturnsNotAuthorizedErrorResult(bool canUpdateAccount, bool isInternal)
        {
            // Arrange
            user.CanUpdateAccount.Returns(canUpdateAccount);
            user.IsInternal.Returns(isInternal);
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = "14373487",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            // Action
            var actualData = controller.UpdateSingle(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public void UpdateSingle_Success_ReturnSuccessResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = "14373487",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = new Result
            {
                Code = 0,
                Message = string.Empty
            };

            mediator.Send(Arg.Any<ParlayMaxPayoutUpdateSingleQuery>()).Returns(result);

            // Action
            var actualData = controller.UpdateSingle(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.Code, data["Code"]);
            Assert.Equal(result.Message, data["Message"]);
        }

        [Fact]
        public void UpdateSingle_Failed_ReturErrorResultWithMessage()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = "14373487",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = Result.CreateErrorResult("Update unsuccessfully.");

            mediator.Send(Arg.Any<ParlayMaxPayoutUpdateSingleQuery>()).Returns(result);

            // Action
            var actualData = controller.UpdateSingle(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.Code, data["Code"]);
            Assert.Equal(result.Message, data["Message"]);
        }

        [Theory]
        [InlineData("14373487, 14373488, 14373489", "")]
        [InlineData("", "<ROOT></ROOT>")]
        [InlineData("", "")]
        public void UpdateMultiple_InValidQuery_ReturnsUnAuthorizedErrorResult(string custIdList, string maxPayoutLimit)
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateMultipleQuery(user)
            {
                CustIdList = custIdList,
                MaxPayoutLimit = maxPayoutLimit
            };

            // Action
            var actualData = controller.UpdateMultiple(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        public void UpdateMultiple_CannotUpdateAccountOrIsInternalOrBoth_ReturnsNotAuthorizedErrorResult(bool canUpdateAccount, bool isInternal)
        {
            // Arrange
            user.CanUpdateAccount.Returns(canUpdateAccount);
            user.IsInternal.Returns(isInternal);
            var message = new ParlayMaxPayoutUpdateMultipleQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            // Action
            var actualData = controller.UpdateMultiple(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public void UpdateMultiple_ValidQueryAndThenCallsMediator_ReturnsCorrectResults()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateMultipleQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var results = new List<Result>
            {
                new Result { Code = 0, Message = string.Empty },
                new Result { Code = 0, Message = string.Empty },
                new Result { Code = 0, Message = string.Empty },
                Result.CreateErrorResult("Update unsuccessfully."),
                Result.CreateErrorResult("Update unsuccessfully.")
            };

            mediator.Send(Arg.Any<ParlayMaxPayoutUpdateMultipleQuery>()).Returns(results);

            // Action
            var actualData = controller.UpdateMultiple(message) as JsonResult;

            // Assert
            AssertExtension.Equal(results, actualData.Data);
        }

        [Theory]
        [InlineData("14373487, 14373488, 14373489", "")]
        [InlineData("", "<ROOT></ROOT>")]
        [InlineData("", "")]
        public void CheckDownline_InValidQuery_ReturnsUnAuthorizedErrorResult(string custIdList, string maxPayoutLimit)
        {
            // Arrange
            var message = new ParlayMaxPayoutCheckDownlineQuery(user)
            {
                CustIdList = custIdList,
                MaxPayoutLimit = maxPayoutLimit
            };

            // Action
            var actualData = controller.CheckDownline(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(BadRequestErrorCode, data["Code"]);
            Assert.Equal(NotAuthorizedErrorMessage, data["Message"]);
        }

        [Fact]
        public void CheckDownline_Success_ReturnsSuccessResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutCheckDownlineQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = new Result
            {
                Code = 0,
                Message = string.Empty
            };

            mediator.Send(Arg.Any<ParlayMaxPayoutCheckDownlineQuery>()).Returns(result);

            // Action
            var actualData = controller.CheckDownline(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.Code, data["Code"]);
            Assert.Equal(result.Message, data["Message"]);
        }

        [Fact]
        public void CheckDownline_Failed_ReturnsErrorResultWithMessage()
        {
            // Arrange
            var message = new ParlayMaxPayoutCheckDownlineQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = Result.CreateErrorResult("Update will override all downlines' settings.");

            mediator.Send(Arg.Any<ParlayMaxPayoutCheckDownlineQuery>()).Returns(result);

            // Action
            var actualData = controller.CheckDownline(message) as JsonResult;
            IDictionary<string, object> data = new RouteValueDictionary(actualData.Data);

            // Assert
            Assert.Equal(result.Code, data["Code"]);
            Assert.Equal(result.Message, data["Message"]);
        }
    }
}
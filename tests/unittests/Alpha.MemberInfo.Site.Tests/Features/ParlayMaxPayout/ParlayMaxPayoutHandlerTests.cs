namespace Alpha.MemberInfo.Site.Tests.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Params;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Site.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Site.Features.ParlayMaxPayout.Requests;
    using Alpha.MemberInfo.Site.Features.ParlayMaxPayout.ViewModels;
    using Alpha.MemberInfo.Site.Tests.Extensions;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutHandlerTests
    {
        private readonly IUser user;
        private readonly ParlayMaxPayoutService parlayMaxPayoutService;
        private readonly CheckUMService checkUMService;
        private readonly ParlayMaxPayoutHandler handler;

        public ParlayMaxPayoutHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(1176070);
            user.Name.Returns("supertest1");
            user.LevelId.Returns((byte)4);
            user.Language.Returns("En");
            user.CanUpdateAccount.Returns(true);
            user.IsInternal.Returns(false);

            parlayMaxPayoutService = Substitute.For<ParlayMaxPayoutService>();
            checkUMService = Substitute.For<CheckUMService>();

            handler = new ParlayMaxPayoutHandler(parlayMaxPayoutService, checkUMService);
        }

        [Fact]
        public void HandleParlayMaxPayoutSingleSelectQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ParlayMaxPayoutSingleSelectQuery(user)
            {
                CustId = 14373487,
                CustName = "masterkz"
            };

            parlayMaxPayoutService
                .GetParlayMaxPayout(Arg.Any<ParlayMaxPayoutSelectParam>())
                .Returns(Enumerable.Empty<ParlayMaxPayoutModel>());

            var expectedViewModel = new ParlayMaxPayoutSinglePageViewModel(
                message,
                Enumerable.Empty<ParlayMaxPayoutModel>());

            // Action
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandleParlayMaxPayoutMultipleSelectQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ParlayMaxPayoutMultipleSelectQuery(user)
            {
                CustIds = "14373487, 14373488, 14373489",
                CustNames = "masterkz, masterkz1, masterkz2"
            };

            parlayMaxPayoutService
                .GetParlayMaxPayout(Arg.Any<ParlayMaxPayoutSelectParam>())
                .Returns(Enumerable.Empty<ParlayMaxPayoutModel>());

            var expectedViewModel = new ParlayMaxPayoutMultiplePageViewModel(
                message,
                Enumerable.Empty<ParlayMaxPayoutModel>());

            // Action
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandleParlayMaxPayoutUpdateSingleQuery_CheckUMIsFailed_ReturnsCheckUMResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = "14373487",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = Result.CreateErrorResult("UM service is on.");

            checkUMService.CheckUM(Arg.Any<CheckUMParam>()).Returns(result);

            // Action
            var actualResult = handler.Handle(message);

            // Assert
            Assert.Equal(result.Code, actualResult.Code);
            Assert.Equal(result.Message, actualResult.Message);
        }

        [Fact]
        public void HandleParlayMaxPayoutUpdateSingleQuery_Success_ReturnsSuccessResult()
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

            checkUMService.CheckUM(Arg.Any<CheckUMParam>()).Returns(result);
            parlayMaxPayoutService.UpdateParlayMaxPayout(Arg.Any<ParlayMaxPayoutUpdateParam>()).Returns(result);

            // Action
            var actualResult = handler.Handle(message);

            // Assert
            Assert.Equal(result.Code, actualResult.Code);
            Assert.Equal(result.Message, actualResult.Message);
        }

        [Fact]
        public void HandleParlayMaxPayoutUpdateSingleQuery_Failed_ReturnsErrorResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateSingleQuery(user)
            {
                CustIdList = "14373487",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var checkUMResult = new Result
            {
                Code = 0,
                Message = string.Empty
            };

            var result = Result.CreateErrorResult("Update failed.");

            checkUMService.CheckUM(Arg.Any<CheckUMParam>()).Returns(checkUMResult);
            parlayMaxPayoutService.UpdateParlayMaxPayout(Arg.Any<ParlayMaxPayoutUpdateParam>()).Returns(result);

            // Action
            var actualResult = handler.Handle(message);

            // Assert
            Assert.Equal(result.Code, actualResult.Code);
            Assert.Equal(result.Message, actualResult.Message);
        }

        [Fact]
        public void HanldeParlayMaxPayoutUpdateMultipleQuery_Always_ReturnsCorrectResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutUpdateMultipleQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var successResult = new Result { Code = 0, Message = string.Empty };

            checkUMService.CheckUM(Arg.Any<CheckUMParam>()).Returns(successResult);
            parlayMaxPayoutService.UpdateParlayMaxPayout(Arg.Any<ParlayMaxPayoutUpdateParam>()).Returns(successResult);

            var expectedResult = new List<Result> { successResult, successResult, successResult };

            // Action
            var actulaResult = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedResult, actulaResult);
        }

        [Fact]
        public void HandleParlayMaxPayoutCheckDownlineQuery_Success_ReturnsSuccessResult()
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

            parlayMaxPayoutService.CheckDownline(Arg.Any<ParlayMaxPayoutCheckDownlineParam>()).Returns(result);

            // Action
            var actualResult = handler.Handle(message);

            // Assert
            Assert.Equal(result.Code, actualResult.Code);
            Assert.Equal(result.Message, actualResult.Message);
        }

        [Fact]
        public void HandleParlayMaxPayoutCheckDownlineQuery_Failed_ReturnsErrorResult()
        {
            // Arrange
            var message = new ParlayMaxPayoutCheckDownlineQuery(user)
            {
                CustIdList = "14373487, 14373488, 14373489",
                MaxPayoutLimit = "<ROOT></ROOT>"
            };

            var result = Result.CreateErrorResult("Update will override all downlines' settings.");

            parlayMaxPayoutService.CheckDownline(Arg.Any<ParlayMaxPayoutCheckDownlineParam>()).Returns(result);

            // Action
            var actualResult = handler.Handle(message);

            // Assert
            Assert.Equal(result.Code, actualResult.Code);
            Assert.Equal(result.Message, actualResult.Message);
        }
    }
}
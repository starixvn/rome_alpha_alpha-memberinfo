namespace Alpha.MemberInfo.Tests.Features.ParlayMaxPayout
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Models;
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Params;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Data.Repository;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutServiceImplTests
    {
        private readonly IDynamicRepository repository;
        private readonly ParlayMaxPayoutService parlayMaxPayoutService;

        public ParlayMaxPayoutServiceImplTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            parlayMaxPayoutService = new ParlayMaxPayoutServiceImpl(repository);
        }

        [Fact]
        public void GetParlayMaxPayout_HasNoData_ReturnsEmptyResult()
        {
            // Arrange
            var param = new ParlayMaxPayoutSelectParam(1176070);

            repository
                .Fetch<ParlayMaxPayoutModel>(Arg.Any<ParlayMaxPayoutSelectCriteria>())
                .Returns(Enumerable.Empty<ParlayMaxPayoutModel>());

            // Action
            var actualResult = parlayMaxPayoutService.GetParlayMaxPayout(param);

            // Assert
            Assert.Empty(actualResult);
        }

        [Fact]
        public void GetParlayMaxPayout_HasData_ReturnsCorrectResult()
        {
            // Arrange
            var param = new ParlayMaxPayoutSelectParam(1176070);

            var expectedResult = new List<ParlayMaxPayoutModel>
            {
                new ParlayMaxPayoutModel(),
                new ParlayMaxPayoutModel(),
                new ParlayMaxPayoutModel()
            };

            repository
                .Fetch<ParlayMaxPayoutModel>(Arg.Any<ParlayMaxPayoutSelectCriteria>())
                .Returns(expectedResult);

            // Action
            var actualResult = parlayMaxPayoutService.GetParlayMaxPayout(param);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "Update unsuccessfully.")]
        public void UpdateParlayMaxPayout_Always_ReturnsCorrectResult(int errCode, string errMsg)
        {
            // Arrange
            var param = new ParlayMaxPayoutUpdateParam(14373487, string.Empty, 1176070, 14373488, "vi", "10.19.10.81");

            repository
                .When(r => r.Execute(Arg.Any<ParlayMaxPayoutUpdateCriteria>()))
                .Do(c =>
                {
                    ((ParlayMaxPayoutUpdateCriteria)c[0]).ErrCode = errCode;
                    ((ParlayMaxPayoutUpdateCriteria)c[0]).ErrMsg = errMsg;
                });

            // Action
            var actualResult = parlayMaxPayoutService.UpdateParlayMaxPayout(param);

            // Assert
            Assert.Equal(errCode, actualResult.Code);
            Assert.Equal(errMsg, actualResult.Message);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "Update unsuccessfully.")]
        public void CheckDownline_Always_ReturnsCorrectResult(int errCode, string errMsg)
        {
            // Arrange
            var param = new ParlayMaxPayoutCheckDownlineParam(14373487, string.Empty, "vi");

            repository
                .When(r => r.Execute(Arg.Any<ParlayMaxPayoutCheckDownlineCriteria>()))
                .Do(c =>
                {
                    ((ParlayMaxPayoutCheckDownlineCriteria)c[0]).ErrCode = errCode;
                    ((ParlayMaxPayoutCheckDownlineCriteria)c[0]).ErrMsg = errMsg;
                });

            // Action
            var actualResult = parlayMaxPayoutService.CheckDownline(param);

            // Assert
            Assert.Equal(errCode, actualResult.Code);
            Assert.Equal(errMsg, actualResult.Message);
        }
    }
}
namespace Alpha.MemberInfo.Tests.Services
{
    using Alpha.MemberInfo.Criteria;
    using Alpha.MemberInfo.Params;
    using Alpha.MemberInfo.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class CheckUMServiceImplTests
    {
        private readonly CacheRefService refService;
        private readonly IDynamicRepository repository;
        private readonly CheckUMService checkUMService;

        public CheckUMServiceImplTests()
        {
            refService = Substitute.For<CacheRefService>();
            repository = Substitute.For<IDynamicRepository>();
            checkUMService = new CheckUMServiceImpl(refService, repository);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "{0}")]
        public void CheckUM_Always_ReturnsCorrectResult(int errCode, string errMsg)
        {
            // Arrange
            var param = new CheckUMParam(14373487, 1, "vi");

            repository
                .When(r => r.Execute(Arg.Any<CheckUMCriteria>()))
                .Do(c =>
                {
                    ((CheckUMCriteria)c[0]).ErrCode = errCode;
                    ((CheckUMCriteria)c[0]).ErrMsg = errMsg;
                });

            refService.GetProductName(Arg.Any<int>()).Returns("Sportbook");

            // Action
            var actualResult = checkUMService.CheckUM(param);

            // Assert
            Assert.Equal(errCode, actualResult.Code);
            Assert.Equal(string.Format(errMsg, "Sportbook"), actualResult.Message);
        }
    }
}
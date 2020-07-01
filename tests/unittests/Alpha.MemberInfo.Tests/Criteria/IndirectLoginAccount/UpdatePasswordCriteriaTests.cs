namespace Alpha.MemberInfo.Tests.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class UpdatePasswordCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange & Act
            var param = new UpdatePasswordParam(
                1176083,
                0,
                "master1",
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);

            // Assert
            Assert.True(
                criteria.UserPwd == param.Password
                && criteria.ActorId == param.ActorId
                && criteria.ActorSubAccId == param.ActorSubAccId
                && criteria.ActorUsername == param.ActorUsername
                && criteria.IP == param.RequestInfo.LoginIP
                && criteria.Language == param.LanguageKey
                && criteria.LoginDomain == param.RequestInfo.LoginDomain
                && criteria.BrowserName == param.RequestInfo.BrowserName
                && criteria.Platform == param.RequestInfo.Platform
                && criteria.CountryCode == param.RequestInfo.CountryCode);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var param = new UpdatePasswordParam(
                1176083,
                0,
                "master1",
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);
            var expected = "Age_CustomerIndirectLoginAccountPwdCheck";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1176083, true)]
        [InlineData(0, false)]
        [InlineData(-1176083, false)]
        public void IsValid_InputIndirectCustId_ReturnsExpectedResult(int indirectCustId, bool expected)
        {
            // Arrange
            var param = new UpdatePasswordParam(
                indirectCustId,
                0,
                "master1",
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(159378, true)]
        [InlineData(0, true)]
        [InlineData(-159378, false)]
        public void IsValid_InputIndirectSubAccId_ReturnsExpectedResult(int indirectSubAccId, bool expected)
        {
            // Arrange
            var param = new UpdatePasswordParam(
                1176083,
                indirectSubAccId,
                "master1",
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("master1", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_InputIndirectUsername_ReturnsExpectedResult(string indirectUsername, bool expected)
        {
            // Arrange
            var param = new UpdatePasswordParam(
                1176083,
                159378,
                indirectUsername,
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(0, false)]
        public void IsValid_IndirectLevelId_ReturnsExpectedResult(byte indirectLevelId, bool expected)
        {
            // Arrange
            var param = new UpdatePasswordParam(
                1176083,
                159378,
                "master1",
                indirectLevelId,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new UpdatePasswordCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class NewAccountCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange & Act
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Assert
            Assert.True(
                criteria.ActorLevelId == param.ActorLevelId
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
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);
            var expected = "Age_CustomerIndirectLoginAccountIns";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsValid_ValidAllParams_ReturnsTrue()
        {
            // Arrange
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(1176070, true)]
        [InlineData(0, false)]
        [InlineData(-1176070, false)]
        public void IsValid_InputActorId_ReturnsExpectedResult(int actorId, bool expected)
        {
            // Arrange
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                actorId,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(125597, true)]
        [InlineData(0, true)]
        [InlineData(-125597, false)]
        public void IsValid_InputActorSubAccId_ReturnsExpectedResult(int actorSubAccId, bool expected)
        {
            // Arrange
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                actorSubAccId,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("master1", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_InputCustName_ReturnsExpectedResult(string custname, bool expected)
        {
            // Arrange
            var param = new NewAccountParam(
                custname,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1qazxsw2", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_InputPassword_ReturnsExpectedResult(string password, bool expected)
        {
            // Arrange
            var param = new NewAccountParam(
                "master1",
                password,
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            var criteria = new NewAccountCriteria(
                param.CustName,
                param.Password,
                param.ActorId,
                param.ActorSubAccId,
                param.ActorLevelId,
                param.LanguageKey,
                param.RequestInfo.LoginIP,
                param.RequestInfo.LoginDomain,
                param.RequestInfo.BrowserName,
                param.RequestInfo.Platform,
                param.RequestInfo.CountryCode);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

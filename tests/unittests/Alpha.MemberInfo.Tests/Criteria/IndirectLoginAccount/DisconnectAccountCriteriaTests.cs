namespace Alpha.MemberInfo.Tests.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class DisconnectAccountCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new DisconnectAccountCriteria(
                1176083, 0, "master1", 1176070, 0, "supertest1", "en-US", "::1");
            var expected = "Age_CustomerIndirectLoginAccountDel";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1176083, true)]
        [InlineData(0, false)]
        [InlineData(-1176083, false)]
        public void IsValid_IndirectCustId_ReturnsExpectedResult(int indirectCustId, bool expected)
        {
            // Arrange
            var criteria = new DisconnectAccountCriteria(
                indirectCustId, 0, "master1", 1176070, 0, "supertest1", "en-US", "::1");

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(159378, true)]
        [InlineData(0, true)]
        [InlineData(-159378, false)]
        public void IsValid_IndirectSubAccId_ReturnsExpectedResult(int indirectSubAccId, bool expected)
        {
            // Arrange
            var criteria = new DisconnectAccountCriteria(
                1176083, indirectSubAccId, "master1", 1176070, 0, "supertest1", "en-US", "::1");

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("master1", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_IndirectUsername_ReturnsExpectedResult(string indirectUsername, bool expected)
        {
            // Arrange
            var criteria = new DisconnectAccountCriteria(
                1176083, 0, indirectUsername, 1176070, 0, "supertest1", "en-US", "::1");

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

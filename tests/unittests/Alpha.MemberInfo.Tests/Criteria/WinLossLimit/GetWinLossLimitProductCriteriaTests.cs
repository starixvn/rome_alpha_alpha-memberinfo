namespace Alpha.MemberInfo.Tests.Criteria.WinLossLimit
{
    using MemberInfo.Criteria.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class GetWinLossLimitProductCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelWinLimitProduct";
            var criteria = new GetWinLossLimitProductCriteria(1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new GetWinLossLimitProductCriteria(custId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

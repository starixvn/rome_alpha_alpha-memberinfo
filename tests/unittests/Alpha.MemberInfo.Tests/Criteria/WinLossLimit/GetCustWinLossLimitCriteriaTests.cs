namespace Alpha.MemberInfo.Tests.Criteria.WinLossLimit
{
    using MemberInfo.Criteria.WinLossLimit;
    using MemberInfo.Params;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class GetCustWinLossLimitCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelCustWinlimit";
            var param = new WinLossLimitParam(1, 1, 1, 1, string.Empty, string.Empty, 1, 1, 1, 1);
            var criteria = new GetCustWinLossLimitCriteria(param, 1);

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
            var param = new WinLossLimitParam(1, custId, 1, 1, string.Empty, string.Empty, 1, 1, 1, 1);
            var criteria = new GetCustWinLossLimitCriteria(param, 1);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

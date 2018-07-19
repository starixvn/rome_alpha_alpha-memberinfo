namespace Alpha.MemberInfo.Tests.Criteria.WinLossLimit
{
    using MemberInfo.Criteria.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class MapPlayerId3rdToCustIdCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustomerSelByPlayerId";
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "2");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsCorrectValue()
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "2");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }
    }
}

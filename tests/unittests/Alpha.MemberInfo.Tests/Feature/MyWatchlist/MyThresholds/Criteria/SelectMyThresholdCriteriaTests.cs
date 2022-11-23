namespace Alpha.MemberInfo.Tests.Feature.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlist")]
    public class SelectMyThresholdCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_SMA_Sel";
            var criteria = new SelectMyThresholdCriteria(14373487, 0);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(14373487, true)]
        [InlineData(0, false)]
        [InlineData(-14373487, false)]
        public void IsValid_CustId_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectMyThresholdCriteria(custId, 0);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange
            var criteria = new SelectMyThresholdCriteria(14373487, 99);

            // Act & Assert
            Assert.True(14373487 == criteria.CustId && 99 == criteria.SportType);
        }
    }
}

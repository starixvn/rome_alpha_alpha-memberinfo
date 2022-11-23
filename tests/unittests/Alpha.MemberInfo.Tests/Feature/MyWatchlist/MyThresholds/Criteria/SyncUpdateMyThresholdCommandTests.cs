namespace Alpha.MemberInfo.Tests.Feature.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlist")]
    public class SyncUpdateMyThresholdCommandTests
    {
        [Theory]
        [InlineData("DB052")]
        [InlineData("DB053")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(string suffix)
        {
            // Arrange
            var expectedSettingKey = $"Acc_CustWLMonitored_Setting_Adjust_{suffix}";
            var custList = "1,2,3,4,5,6";
            var criteria = new SyncUpdateMyThresholdCommand(custList, suffix);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("1,2,3,4,5,6", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_CustId_ReturnsCorrectValue(string custList, bool expectedValue)
        {
            // Arrange
            var criteria = new SyncUpdateMyThresholdCommand(custList, "DB052");

            // Act && Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

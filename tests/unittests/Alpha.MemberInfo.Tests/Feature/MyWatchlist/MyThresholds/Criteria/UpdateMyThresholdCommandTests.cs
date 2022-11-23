namespace Alpha.MemberInfo.Tests.Feature.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Xunit;

    [Trait("Category", "MyWatchlist")]
    public class UpdateMyThresholdCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_SMA_Adjust";
            var param = new UpdateMyThresholdParam(
                14373487,
                1000,
                100,
                14375009,
                0,
                "::1");
            var criteria = new UpdateMyThresholdCommand(param);

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
            var param = new UpdateMyThresholdParam(
                custId,
                1000,
                100,
                14375009,
                0,
                "::1");
            var criteria = new UpdateMyThresholdCommand(param);

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

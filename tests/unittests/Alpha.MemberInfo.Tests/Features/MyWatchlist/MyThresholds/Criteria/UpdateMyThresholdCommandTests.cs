namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Xunit;

    [Trait("Category", "MyThresholds")]
    public class UpdateMyThresholdCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var param = new UpdateMyThresholdParam(
                1234, null, 100, 5678, 0, "::1", string.Empty);
            var criteria = new UpdateMyThresholdCommand(
                param.CustId,
                param.SportType,
                param.WinningThreshold,
                param.LosingThreshold,
                param.UserId,
                param.SubAccId,
                param.IP);

            // Act & Assert
            Assert.Equal("Acc_WLMonitored_SMA_Adjust", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(1234, true)]
        [InlineData(-1234, false)]
        [InlineData(0, false)]
        public void IsValid_CustId_ReturnsCorrectExpectedValue(int custId, bool expectedValue)
        {
            // Arrange
            var param = new UpdateMyThresholdParam(
                custId, null, 100, 5678, 0, "::1", string.Empty);
            var criteria = new UpdateMyThresholdCommand(
                param.CustId,
                param.SportType,
                param.WinningThreshold,
                param.LosingThreshold,
                param.UserId,
                param.SubAccId,
                param.IP);

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

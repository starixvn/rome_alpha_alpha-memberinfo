namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Xunit;

    [Trait("Category", "MyThresholds")]
    public class SyncUpdateMyThresholdCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var suffix = "DB053";
            var criteria = new SyncUpdateMyThresholdCommand("1,2,3,4,5,6", suffix);

            // Act & Assert
            Assert.Equal($"Acc_CustWLMonitored_Setting_Adjust_{suffix}", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData("1,2,3,4,5,6", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("     ", false)]
        public void IsValid_CustId_ReturnsCorrectExptectedValue(string custIds, bool expectedValue)
        {
            // Arrange
            var criteria = new SyncUpdateMyThresholdCommand(custIds, "DB053");

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

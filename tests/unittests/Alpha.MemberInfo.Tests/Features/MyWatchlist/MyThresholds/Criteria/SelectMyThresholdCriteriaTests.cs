namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Xunit;

    [Trait("Category", "MyThresholds")]
    public class SelectMyThresholdCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectMyThresholdCriteria(1176083, 0);

            // Act & Assert
            Assert.Equal("Acc_WLMonitored_SMA_Sel", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(1176083, true)]
        [InlineData(-1176083, false)]
        [InlineData(0, false)]
        public void IsValid_CustId_ReturnsCorrectExptectedValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectMyThresholdCriteria(custId, 0);

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

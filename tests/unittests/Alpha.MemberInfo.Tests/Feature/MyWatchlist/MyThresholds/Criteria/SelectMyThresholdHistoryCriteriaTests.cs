namespace Alpha.MemberInfo.Tests.Feature.MyWatchlist.MyThresholds.Criteria
{
    using System;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Xunit;

    [Trait("Category", "MyWatchlist")]
    public class SelectMyThresholdHistoryCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_SMA_ReachDetail_Sel";
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                14373487,
                today.AddDays(-1),
                today,
                1,
                false,
                DateTime.MinValue);
            var criteria = new SelectMyThresholdHistoryCriteria(param);

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
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                custId,
                today.AddDays(-1),
                today,
                1,
                false,
                DateTime.MinValue);
            var criteria = new SelectMyThresholdHistoryCriteria(param);

            // Act && Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Theory]
        [InlineData("2022-12-30", "2022-12-31", true)]
        [InlineData("2022-12-31", "2022-12-31", true)]
        [InlineData("2022-12-30", "2022-12-01", false)]
        public void IsValid_FromTo_ReturnsCorrectValue(string from, string to, bool expectedValue)
        {
            // Arrange
            var param = new SelectMyThresholdHistoryParam(
                14373487,
                DateTime.Parse(from),
                DateTime.Parse(to),
                1,
                false,
                DateTime.MinValue);
            var criteria = new SelectMyThresholdHistoryCriteria(param);

            // Act && Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectProperties()
        {
            // Arrange
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                14373487,
                today.AddDays(-1),
                today,
                1,
                false,
                DateTime.MinValue);
            var criteria = new SelectMyThresholdHistoryCriteria(param);

            // Act & Assert
            Assert.True(param.CustId == criteria.CustId
                && param.FromDate == criteria.FromDate
                && param.ToDate == criteria.ToDate
                && param.SortType == criteria.SortType
                && param.IsDesc == criteria.IsDesc
                && param.SportType == criteria.SportType);
        }
    }
}

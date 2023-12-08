namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyThresholds.Criteria
{
    using System;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Xunit;

    [Trait("Category", "MyThresholds")]
    public class SelectMyThresholdHistoryCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                1176083, today.AddDays(-1), today, 1, true, DateTime.MinValue, string.Empty);
            var criteria = new SelectMyThresholdHistoryCriteria(
                param.CustId,
                param.SportType,
                param.FromDate,
                param.ToDate,
                param.SortType,
                param.IsDesc);

            // Act & Assert
            Assert.Equal("Acc_WLMonitored_SMA_ReachDetail_Sel", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(1176083, true)]
        [InlineData(-1176083, false)]
        [InlineData(0, false)]
        public void IsValid_CustId_ReturnsCorrectExptectedValue(int custId, bool expectedValue)
        {
            // Arrange
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                custId, today.AddDays(-1), today, 1, true, DateTime.MinValue, string.Empty);
            var criteria = new SelectMyThresholdHistoryCriteria(
                param.CustId,
                param.SportType,
                param.FromDate,
                param.ToDate,
                param.SortType,
                param.IsDesc);

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Theory]
        [InlineData("2023-01-08", "2023-01-09", true)]
        [InlineData("2023-01-09", "2023-01-09", true)]
        [InlineData("2023-01-09", "2023-01-01", false)]
        public void IsValid_FromToDate_ReturnsCorrectExptectedValue(string from, string to, bool expectedValue)
        {
            // Arrange
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                1176083, DateTime.Parse(from), DateTime.Parse(to), 1, true, DateTime.MinValue, string.Empty);
            var criteria = new SelectMyThresholdHistoryCriteria(
                param.CustId,
                param.SportType,
                param.FromDate,
                param.ToDate,
                param.SortType,
                param.IsDesc);

            // Act & Assert
            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}

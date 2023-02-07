namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.ReachedThreshold.Criteria
{
    using System;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class SelectReachListByAgCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var today = DateTime.Now;
            var criteria = new SelectReachListByAgCriteria(1234, today.AddDays(-1), today, 1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal("Acc_WLMonitored_Player_ReachList_ByAg", actualSettingKey);
        }

        [Theory]
        [InlineData(1234, true)]
        [InlineData(-1234, false)]
        [InlineData(0, false)]
        public void IsValid_AgentId_ReturnCorrectExpectedValue(int agentId, bool expectedValue)
        {
            // Arrange
            var today = DateTime.Now;
            var criteria = new SelectReachListByAgCriteria(agentId, today.AddDays(-1), today, 1);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData("2023-01-08", "2023-01-09", true)]
        [InlineData("2023-01-09", "2023-01-09", true)]
        [InlineData("2023-01-09", "2023-01-01", false)]
        public void IsValid_FromToDate_ReturnCorrectExpectedValue(string from, string to, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectReachListByAgCriteria(1234, DateTime.Parse(from), DateTime.Parse(to), 1);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.ReachedThreshold.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using System;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class SelectReachListCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int agentId = 1234;
            string username = "agent0101";
            string status = "Open";
            DateTime fromDate = DateTime.Today.AddDays(-1);
            DateTime toDate = DateTime.Today;
            byte limitType = 0;
            byte sortType = 0;
            bool isDesc = false;
            int pageSize = 100;
            int pageIndex = 1;

            // Act
            var criteria = new SelectReachListCriteria(agentId, username, fromDate, toDate, status, limitType, sortType, isDesc, pageSize, pageIndex);

            // Assert
            Assert.True(
                criteria.AgentId == agentId
                && criteria.Username == username
                && criteria.Status == status
                && criteria.FromDate == fromDate
                && criteria.ToDate == toDate
                && criteria.LimitType == limitType
                && criteria.SortType == sortType
                && criteria.IsDesc == isDesc
                && criteria.PageSize == pageSize
                && criteria.PageIndex == pageIndex
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Player_ReachList_Sel";
            var criteria = new SelectReachListCriteria(1234, "agent0101", DateTime.Today.AddDays(-1), DateTime.Today, "Open", 0, 0, false, 100, 1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, "2022/1/1", "2022/1/1", true)]
        [InlineData(1, "2022/1/2", "2022/1/1", false)]
        [InlineData(-1, "2022/1/1", "2022/1/1", false)]
        public void IsValid_Always_ReturnCorrectValue(int agentId, DateTime fromDate, DateTime toDate, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectReachListCriteria(agentId, "agent0101", fromDate, toDate, "Open", 0, 0, false, 100, 1);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

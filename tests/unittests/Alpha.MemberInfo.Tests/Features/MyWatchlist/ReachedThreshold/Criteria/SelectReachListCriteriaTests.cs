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
            byte sortType = 0;
            bool isDesc = false;
            int pageSize = 100;
            int pageIndex = 1;
            string xmlReachList = "test";

            // Act
            var criteria = new SelectReachListCriteria(agentId, username, status, sortType, isDesc, pageSize, pageIndex, xmlReachList);

            // Assert
            Assert.True(
                criteria.AgentId == agentId
                && criteria.Username == username
                && criteria.Status == status
                && criteria.SortType == sortType
                && criteria.IsDesc == isDesc
                && criteria.PageSize == pageSize
                && criteria.PageIndex == pageIndex
                && criteria.XMLReachList == xmlReachList
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Player_ReachList_Sel";
            var criteria = new SelectReachListCriteria(1234, "agent0101", "Open", 0, false, 100, 1, "test");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnCorrectValue()
        {
            // Arrange
            var criteria = new SelectReachListCriteria(1, "agent0101", "Open", 0, false, 100, 1, "test");

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }
    }
}

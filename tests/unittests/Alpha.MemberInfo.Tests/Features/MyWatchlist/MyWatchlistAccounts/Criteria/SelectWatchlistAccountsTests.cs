namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using System;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class SelectWatchlistAccountsTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int agentId = 1234;
            string username = "agent01";
            byte sortType = 0;
            bool isDesc = true;
            int pageIndex = 1;
            int pageSize = 100;

            // Act
            var criteria = new SelectWatchlistAccountsCriteria(
                agentId, DateTime.Now, DateTime.Now, string.Empty, 2 ,username, sortType, isDesc, pageIndex, pageSize);

            // Assert
            Assert.True(
                criteria.AgentId == agentId
                && criteria.Username == username
                && criteria.SortType == sortType
                && criteria.IsDesc == isDesc
                && criteria.PageIndex == pageIndex
                && criteria.PageSize == pageSize
                && criteria.TotalItems == default(int)
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Player_WatchList_Sel";
            var criteria = new SelectWatchlistAccountsCriteria(1234, DateTime.Now, DateTime.Now, string.Empty, 2, "agent01",  0, true, 1, 100);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_Always_ReturnCorrectValue(int agentId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectWatchlistAccountsCriteria(agentId, DateTime.Now, DateTime.Now, string.Empty, 2, "agent01", 0, true, 1, 100);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

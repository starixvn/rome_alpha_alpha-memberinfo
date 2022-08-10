namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.ReachedThreshold.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using System;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class SelectReachDetailCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int custId = 1234;
            DateTime fromDate = DateTime.Today.AddDays(-1);
            DateTime toDate = DateTime.Today;
            byte limitType = 0;
            byte sortType = 0;
            bool isDesc = false;

            // Act
            var criteria = new SelectReachDetailCriteria(custId, fromDate, toDate, limitType, sortType, isDesc);

            // Assert
            Assert.True(
                criteria.CustId == custId
                && criteria.FromDate == fromDate
                && criteria.ToDate == toDate
                && criteria.LimitType == limitType
                && criteria.SortType == sortType
                && criteria.IsDesc == isDesc
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Player_ReachDetail_Sel";
            var criteria = new SelectReachDetailCriteria(1234, DateTime.Today.AddDays(-1), DateTime.Today, 0, 0, false);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, "2022/1/1", "2022/1/1", true)]
        [InlineData(1, "2022/1/2", "2022/1/1", false)]
        [InlineData(-1, "2022/1/1", "2022/1/1", false)]
        public void IsValid_Always_ReturnCorrectValue(int custId, DateTime fromDate, DateTime toDate,bool expectedValue)
        {
            // Arrange
            var criteria = new SelectReachDetailCriteria(custId, fromDate, toDate, 1, 1, true);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

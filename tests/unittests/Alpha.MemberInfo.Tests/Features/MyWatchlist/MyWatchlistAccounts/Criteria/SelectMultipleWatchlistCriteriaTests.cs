namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class SelectMultipleWatchlistCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            string custList = "member01,member02";

            // Act
            var criteria = new SelectMultipleWatchlistCriteria(custList);

            // Assert
            Assert.True(criteria.CustList == custList);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_CheckExistSettingByCust";
            var criteria = new SelectMultipleWatchlistCriteria("member01,member02");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("member01,member02", true)]
        [InlineData(" ", false)]
        [InlineData("", false)]
        public void IsValid_Always_ReturnCorrectValue(string custList, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectMultipleWatchlistCriteria(custList);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

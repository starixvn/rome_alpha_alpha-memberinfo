namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class DeletePersonalThresholdCommandTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int custId = 1234;
            int userId = 1;
            int subAccId = 12;
            string ip = "192.0.0.1";

            // Act
            var criteria = new DeletePersonalThresholdCommand(custId, userId, subAccId, ip);

            // Assert
            Assert.True(
                criteria.CustId == custId
                && criteria.UserId == userId
                && criteria.SubAccId == subAccId
                && criteria.IP == ip
                && criteria.ErrCode == default(int)
                && criteria.ErrMsg == default(string)
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Personal_Del";
            var criteria = new DeletePersonalThresholdCommand(1234, 1, 12, "192.0.0.1");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_Always_ReturnCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new DeletePersonalThresholdCommand(custId, 1, 12, "192.0.0.1");

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class UpdatePersonalThresholdCommandTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int custId = 1234;
            decimal? winningThreshold = 1000;
            decimal? losingThreshold = 2000;
            int userId = 1;
            int subAccId = 12;
            string ip = "192.0.0.1";

            // Act
            var criteria = new UpdatePersonalThresholdCommand(custId, winningThreshold, losingThreshold, userId, subAccId, ip);

            // Assert
            Assert.True(
                criteria.CustId == custId
                && criteria.WinningThreshold == winningThreshold
                && criteria.LosingThreshold == losingThreshold
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
            var expectedSettingKey = "Acc_WLMonitored_Personal_Upd";
            var criteria = new UpdatePersonalThresholdCommand(1234, 1000, 2000, 1, 12, "192.0.0.1");

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
            var criteria = new UpdatePersonalThresholdCommand(custId, 1000, 2000, 1, 12, "192.0.0.1");

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}


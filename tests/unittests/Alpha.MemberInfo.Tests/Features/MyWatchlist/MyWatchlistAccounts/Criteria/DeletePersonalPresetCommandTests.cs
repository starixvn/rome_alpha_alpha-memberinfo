namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class DeletePersonalPresetCommandTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int agentId = 1234;
            int id = 1;

            // Act
            var criteria = new DeletePersonalPresetCommand(agentId, id);

            // Assert
            Assert.True(
                criteria.AgentId == agentId
                && criteria.ID == id
                && criteria.ErrCode == default(int)
                && criteria.ErrMsg == default(string)
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Preset_Del";
            var criteria = new DeletePersonalPresetCommand(1234, 1);

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
            var criteria = new DeletePersonalPresetCommand(agentId, 1);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

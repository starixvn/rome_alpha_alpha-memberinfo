namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class UpdatePersonalPresetCommandTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int agentId = 1234;
            int id = 1;
            string presetTitle = "preset1";
            decimal winningThreshold = 1000;
            decimal losingThreshold = 2000;

            // Act
            var criteria = new UpdatePersonalPresetCommand(agentId, id, presetTitle, winningThreshold, losingThreshold);

            // Assert
            Assert.True(
                criteria.AgentId == agentId
                && criteria.ID == id
                && criteria.PresetTitle == presetTitle
                && criteria.WinningThreshold == winningThreshold
                && criteria.LosingThreshold == losingThreshold
                && criteria.ErrCode == default(int)
                && criteria.ErrMsg == default(string)
                && criteria.Sporttype == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_WLMonitored_Preset_Upd";
            var criteria = new UpdatePersonalPresetCommand(1234, 1, "preset1", 1000, 2000);

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
            var criteria = new UpdatePersonalPresetCommand(agentId, 1, "preset1", 1000, 2000);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class PersonalPresetModelTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int id = 1;
            int sporttype= 1;
            string presetTitle = "preset1";
            decimal winningThreshold = 1000;
            decimal losingThreshold = 2000;

            // Act
            var model = new PersonalPresetModel(id, sporttype, presetTitle, winningThreshold, losingThreshold);

            // Assert
            Assert.True(model.ID == id
                && model.Sporttype == sporttype
                && model.PresetTitle == presetTitle
                && model.WinningThreshold == winningThreshold
                && model.LosingThreshold == losingThreshold);
        }
    }
}

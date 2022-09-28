namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class GlobalThresholdModelTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            decimal? winningThreshold = 1000;
            decimal? losingThreshold = 2000;

            // Act
            var model = new GlobalThresholdModel(winningThreshold, losingThreshold);

            // Assert
            Assert.True(model.WinningThreshold == winningThreshold && model.LosingThreshold == losingThreshold);
        }
    }
}

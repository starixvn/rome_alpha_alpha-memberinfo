namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class MultipleWatchlistAccountModelTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            List<WatchlistAccountModel> alreadyAddedAccounts = new List<WatchlistAccountModel>()
            {
                new WatchlistAccountModel(),
                new WatchlistAccountModel(),
                new WatchlistAccountModel(),
            };
            List<WatchlistAccountModel> newlyAddedAccounts = new List<WatchlistAccountModel>(){
                new WatchlistAccountModel(),
                new WatchlistAccountModel(),
            };

            // Act
            var model = new MultipleWatchlistAccountModel(alreadyAddedAccounts, newlyAddedAccounts);

            // Assert
            Assert.True(model.AlreadyAddedAccounts.Count() == 3&& model.NewlyAddedAccounts.Count() == 2);
        }
    }
}

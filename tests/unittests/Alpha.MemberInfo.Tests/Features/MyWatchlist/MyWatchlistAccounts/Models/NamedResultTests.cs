namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class NamedResultTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            string name = "agent01";
            int code = 0;
            string message = "Add successfully";

            // Act
            var model = new NamedResult(name, code, message);

            // Assert
            Assert.True(model.Name == name && model.Code == code && model.Message == message);
        }
    }
}

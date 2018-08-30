namespace Alpha.MemberInfo.Tests.Helpers
{
    using MemberInfo.Helpers;
    using Xunit;

    [Trait("Category", "MemberInfo")]
    public class ProductHelperTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(33, true)]
        public void IsPokerGames_Always_ReturnsCorrectValue(int productId, bool expectedValue)
        {
            // Act
            var actualValue = productId.IsPokerGames();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

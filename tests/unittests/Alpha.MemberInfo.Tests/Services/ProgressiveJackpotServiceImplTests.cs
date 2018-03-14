namespace Alpha.MemberInfo.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using MemberInfo.Models;
    using MemberInfo.Services.ProgressiveJackpot;
    using NSubstitute;
    using RemotingRef;
    using Xunit;

    public class ProgressiveJackpotServiceImplTests
    {
        private readonly CacheRefService cacheRefService;
        private readonly ProgressiveJackpotServiceImpl progressiveJackpotService;

        public ProgressiveJackpotServiceImplTests()
        {
            cacheRefService = Substitute.For<CacheRefService>();
            cacheRefService.GetProductName(6).Returns("Casino");

            progressiveJackpotService = new ProgressiveJackpotServiceImpl(cacheRefService);
        }

        [Fact]
        public void GetJackpotProducts_EmptyProductIds_ReturnsEmptyProductList()
        {
            // Arrange
            var expected = new List<Product>();

            // Act
            var actual = progressiveJackpotService.GetJackpotProducts(Enumerable.Empty<int>());

            // Assert
            AssertExtension.Equal(expected, actual);
        }

        [Fact]
        public void GetJackpotProducts_HasDataProductIds_ReturnsCorrectProductList()
        {
            // Arrange
            var expected = new List<Product> {
                new Product(6,"Casino",false)
            };

            // Act
            var actual = progressiveJackpotService.GetJackpotProducts(new List<int> { 6 });

            // Assert
            AssertExtension.Equal(expected, actual);
        }
    }
}
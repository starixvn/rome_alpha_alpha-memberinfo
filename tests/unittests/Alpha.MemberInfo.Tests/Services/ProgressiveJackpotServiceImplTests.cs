namespace Alpha.MemberInfo.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Params;
    using Extensions;
    using MemberInfo.Models;
    using MemberInfo.Services;
    using MemberInfo.Services.ProgressiveJackpot;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "ProgressiveJackpot")]
    public class ProgressiveJackpotServiceImplTests
    {
        private readonly CacheRefService cacheRefService;
        private readonly ProgressiveJackpotServiceImpl progressiveJackpotService;

        public ProgressiveJackpotServiceImplTests()
        {
            cacheRefService = Substitute.For<CacheRefService>();
            cacheRefService.GetProductName(6).Returns("Casino");
            Func<int, bool> canViewProduct = (productId) => true;
            progressiveJackpotService = new ProgressiveJackpotServiceImpl(cacheRefService, canViewProduct);
        }

        [Fact]
        public void FilterProductIds_Always_ReturnsCorrectList()
        {
            // Arrange
            var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            var actual = progressiveJackpotService.FilterProductIds(expected);

            // Assert
            AssertExtension.Equal(expected, actual);
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

        [Fact]
        public void GetJackpotGameSettings_DoNotContainCasino_ReturnsEmptyList()
        {
            // Arrange
            var param = new ProgressiveJackpotParam(123, "en-US", new List<int> { 1, 2, 3, 4, 5 });

            // Act
            var actual = progressiveJackpotService.GetJackpotGameSettings(param);

            // Assert
            Assert.Empty(actual);
        }
    }
}
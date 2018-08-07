namespace Alpha.MemberInfo.Tests.Models.CreditBalance
{
    using System.Collections.Generic;
    using System.Linq;
    using MemberInfo.Models.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class PagedCreditBalanceTests
    {
        private const int PageIndexTest = 1;
        private const int PageSizeTest = 500;
        private const int TotalItemsTest = 587;
        private readonly IEnumerable<CreditBalanceItem> CreditBalancesTest = Enumerable.Empty<CreditBalanceItem>();

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new PagedCreditBalance(
                PageIndexTest,
                PageSizeTest,
                TotalItemsTest,
                CreditBalancesTest);

            // Act & Assert
            Assert.True(
                model.PageIndex == PageIndexTest
                && model.PageSize == PageSizeTest
                && model.TotalItems == TotalItemsTest
                && model.CreditBalances == CreditBalancesTest
                && model.ShowPager == true);
        }

        [Fact]
        public void Constructor_TotalItemsIsZero_ReturnsShowPagerFalse()
        {
            // Arrange
            var model = new PagedCreditBalance(
                PageIndexTest,
                PageSizeTest,
                0,
                CreditBalancesTest);

            // Act & Assert
            Assert.False(model.ShowPager);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Models.WinLossLimit
{
    using MemberInfo.Models.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class WinLossLimitProductModelTests
    {
        [Fact]
        public void DefaultConstructor_Always_NotSetData()
        {
            // Act
            var model = new WinLossLimitProductModel();

            // Assert
            Assert.True(model.BetType == 0 && model.SportType == 0 && model.ProductName == null);
        }

        [Fact]
        public void Constructor_Always_SetData()
        {
            // Act
            var model = new WinLossLimitProductModel(1, 2, "ProductName", 3, 0, 0);

            // Assert
            Assert.True(model.BetType == 1 && model.SportType == 2 && model.ProductName == "ProductName" && model.ProductId == 3);
        }
    }
}

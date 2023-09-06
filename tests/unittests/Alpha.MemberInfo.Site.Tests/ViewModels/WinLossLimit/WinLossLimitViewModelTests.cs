namespace Alpha.MemberInfo.Site.Tests.ViewModels.WinLossLimit
{
    using System.Linq;
    using Fixtures;
    using MemberInfo.Models;
    using MemberInfo.Models.WinLossLimit;
    using Params;
    using Site.Requests.WinLossLimit;
    using Site.ViewModels.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    [Collection("UserCollection")]
    public class WinLossLimitViewModelTests
    {
        private readonly UserFixture userFixture;

        public WinLossLimitViewModelTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        public void HasData_Always_ReturnCorrectValue(int numberOfRows, bool expectValue)
        {
            // Arrange
            var viewModel = CreateWinLossLimitViewModel(numberOfRows);

            // Act
            var actualValue = viewModel.HasData;

            // Assert
            Assert.Equal(expectValue, actualValue);
        }

        [Theory]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(10, false)]
        [InlineData(1, false)]
        public void ShowPager_Always_ReturnCorrectValue(int numberOfRows, bool expectValue)
        {
            // Arrange
            var viewModel = CreateWinLossLimitViewModel(numberOfRows);

            // Act
            var actualValue = viewModel.ShowPager;

            // Assert
            Assert.Equal(expectValue, actualValue);
        }

        [Theory]
        [InlineData(1, 13)]
        public void NumberOfColumns_Always_ReturnCorrectValue(int productId, int expectValue)
        {
            // Arrange
            var viewModel = CreateWinLossLimitViewModel(productId: productId);

            // Act
            var actualValue = viewModel.NumberOfColumns;

            // Assert
            Assert.Equal(expectValue, actualValue);
        }

        [Theory]
        [InlineData("Disabled", "Disabled")]
        [InlineData("Closed", "Closed")]
        [InlineData("Suspended", "Suspended")]
        [InlineData("Open", "Open")]
        public void GetStatusText_Always_ReturnCorrectValue(string status, string expectedText)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetStatusText(status);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetCurrentWinAnmount_NullCurrentWinAmount_ReturnUnlimited()
        {
            // Arrange
            var expectedText = "Unlimited";

            // Act
            var actualText = WinLossLimitViewModel.GetCurrentWinAnmout(null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(0, "0.00")]
        [InlineData(1, "1.00")]
        [InlineData(1000, "1,000.00")]
        [InlineData(1000000, "1,000,000.00")]
        public void GetCurrentWinAnmount_NotNullCurrentWinAmount_ReturnCorrectValue(decimal currentWinAnmout, string expectedText)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetCurrentWinAnmout(currentWinAnmout);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(0, "Unlimited")]
        [InlineData(1, "1X")]
        public void GetMaxWinLimit_Always_ReturnCorrectValue(decimal maxWinLimit, string expectedText)
        {
            // Arrange
            var viewModel = CreateWinLossLimitViewModel();

            // Act
            var actualText = viewModel.GetMaxWinLimit(maxWinLimit);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetWinPercent_NullWinPercent_ReturnUnlimited()
        {
            // Arrange
            var expectedText = "Unlimited";
            var viewModel = CreateWinLossLimitViewModel();

            // Act
            var actualText = viewModel.GetWinPercent(null, null, 1,  0);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(1, "1%")]
        [InlineData(1.02, "1.02%")]
        public void GetWinPercent_PositiveWinPercent_ReturnCorrectValue(decimal winPercent, string expectedText)
        {
            // Arrange
            var viewModel = CreateWinLossLimitViewModel();

            // Act
            var actualText = viewModel.GetWinPercent(winPercent, null, 1, 0);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(0)]
        public void GetWinPercent_NotPositiveWinPercent_ReturnCorrectValue(decimal winPercent)
        {
            // Arrange
            var expectedText = "0%";
            var viewModel = CreateWinLossLimitViewModel();

            // Act
            var actualText = viewModel.GetWinPercent(winPercent, null, 1, 0);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetLossPercent_NullLossLimit_ReturnCorrectValue()
        {
            // Arrange
            var expectedText = "-";

            // Act
            var actualText = WinLossLimitViewModel.GetLossPercent(null, null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetLossPercent_NotNullLossLimitAndNullLossPercent_ReturnCorrectValue()
        {
            // Arrange
            var expectedText = "Unlimited";

            // Act
            var actualText = WinLossLimitViewModel.GetLossPercent(10, null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(1, "1%")]
        [InlineData(1.02, "1.02%")]
        [InlineData(0, "0%")]
        public void GetLossPercent_NotNullLossLimitAndPositiveLossPercent_ReturnCorrectValue(decimal lossPercent, string expectedText)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetLossPercent(10, lossPercent);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void GetLossPercent_NotNullLossLimitAndNegativeLossPercent_ReturnCorrectValue(decimal lossPercent)
        {
            // Arrange
            var expectedText = "0%";

            // Act
            var actualText = WinLossLimitViewModel.GetLossPercent(10, lossPercent);

            // Assert
            Assert.Equal(expectedText, actualText);
        }


        [Fact]
        public void GetMaxLossLimit_NullMaxLoss_ReturnCorrectValue()
        {
            // Arrange
            var expectedText = "-";

            // Act
            var actualText = WinLossLimitViewModel.GetMaxLossLimit(null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetMaxLossLimit_ZeroLossPercent_ReturnUnlimited()
        {
            // Arrange
            var expectedText = "Unlimited";

            // Act
            var actualText = WinLossLimitViewModel.GetMaxLossLimit(0);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(1.02, "1.02")]
        public void GetMaxLossLimit_MaxLossHaveValue_ReturnCorrectValue(decimal maxLoss, string expectedText)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetMaxLossLimit(maxLoss);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetRemainingBuyInAmount_NullRemainingBuyInAmount_ReturnCorrectValue()
        {
            // Arrange
            var expectedText = "-";

            // Act
            var actualText = WinLossLimitViewModel.GetRemainingBuyInAmount(null, null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetRemainingBuyInAmount_UnlimitedMaxLoss_ReturnCorrectValue()
        {
            // Arrange
            var expectedText = "Unlimited";

            // Act
            var actualText = WinLossLimitViewModel.GetRemainingBuyInAmount(null, 0);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(1.02, "1.02")]
        public void GetRemainingBuyInAmount_NotNullRemainingBuyInAmount_ReturnCorrectValue(
            decimal remainingBuyInAmount, string expectedText)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetRemainingBuyInAmount(remainingBuyInAmount, null);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void GetPlayerIdText_NullOrWhiteSpace_ReturnCorrectValue(string playerId)
        {
            // Act
            var actualText = WinLossLimitViewModel.GetPlayerIdText(playerId);
            var expectedText = "-";

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public void GetPlayerIdText_NotNullOrWhiteSpace_ReturnCorrectValue()
        {
            // Arrange
            var playerId = "playerId";
            var expectedText = "playerId";

            // Act
            var actualText = WinLossLimitViewModel.GetPlayerIdText(playerId);

            // Assert
            Assert.Equal(expectedText, actualText);
        }

        private WinLossLimitViewModel CreateWinLossLimitViewModel(
            int numberOfRows = 0,
            int productId = 0)
        {
            var query = new WinLossLimitQuery(userFixture.User);
            var param = new WinLossLimitParam(productId, 0, 1, 1, string.Empty, "CP123", 1, 1, 1, 1);

            return new WinLossLimitViewModel(
                query,
                Enumerable.Empty<WinLossLimitGroupProductModel>(),
                Enumerable.Empty<CustomerInfo>(),
                Enumerable.Empty<CustomerInfo>(),
                new WinLossLimitPage(numberOfRows, Enumerable.Empty<WinLossLimitDetail>()),
                param);
        }
    }
}

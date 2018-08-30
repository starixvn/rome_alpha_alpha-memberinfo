namespace Alpha.MemberInfo.Tests.Models.WinLossLimit
{
    using MemberInfo.Models.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class WinLossLimitDetailTests
    {
        [Fact]
        public void CurrentWinAnmout_HaveMaxWinLossLimit_ReturnCorrectValue()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 1,
                CreditBalance = 3,
                DebitBalance = 2
            };

            var expectedValue = 1;

            // Act
            var actualValue = model.CurrentWinAnmout;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void CurrentWinAnmout_ZeroMaxWinAndNullMaxLoss_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 0,
                MaxLossLimit = null,
                CreditBalance = 3,
                DebitBalance = 2
            };

            // Act
            var actualValue = model.CurrentWinAnmout;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void CurrentWinAnmout_ZeroMaxWinAndZeroMaxLoss_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 0,
                MaxLossLimit = 0,
                CreditBalance = 3,
                DebitBalance = 2
            };

            // Act
            var actualValue = model.CurrentWinAnmout;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void CurrentWinAnmout_HaveMaxWinLimitAndNullMaxLoss_ReturnCorrectValue()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 1,
                MaxLossLimit = null,
                CreditBalance = 3,
                DebitBalance = 2
            };

            var expectedValue = 1;

            // Act
            var actualValue = model.CurrentWinAnmout;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void CurrentWinAnmout_HaveMaxWinLimitAndZeroMaxLoss_ReturnCorrectValue()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 1,
                MaxLossLimit = 0,
                CreditBalance = 3,
                DebitBalance = 2
            };

            var expectedValue = 1;

            // Act
            var actualValue = model.CurrentWinAnmout;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WinPercent_ZeroMaxWin_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail { MaxWinLimit = 0 };

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void WinPercent_PositiveBalanceAndZeroCreditAndPositiveMaxWin_ReturnCorrectValue()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 10,
                Credit = 0,
                CreditBalance = 10,
                DebitBalance = 8
            };
            var expectedValue = 200;

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WinPercent_PositiveBalanceAndCreditAndMaxWin_ReturnCorrectValue()
        {
            // Arrange
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 10,
                Credit = 5,
                CreditBalance = 10,
                DebitBalance = 8
            };
            var expectedValue = 4;

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WinPercent_NegativeBalance_ReturnZero()
        {
            // Arrange
            var expectedValue = 0;
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 10,
                Credit = 5,
                CreditBalance = 10,
                DebitBalance = 12
            };

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WinPercent_NegativeCredit_ReturnZero()
        {
            // Arrange
            var expectedValue = 0;
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = 10,
                Credit = -5,
                CreditBalance = 10,
                DebitBalance = 12
            };

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WinPercent_NonPositiveMaxWin_ReturnZero()
        {
            // Arrange
            var expectedValue = 0;
            var model = new WinLossLimitDetail
            {
                MaxWinLimit = -10,
                Credit = -5,
                CreditBalance = 10,
                DebitBalance = 12
            };

            // Act
            var actualValue = model.WinPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Status_NotEnable_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Disabled";
            var model = new WinLossLimitDetail { Enabled = false };

            // Act
            var actualValue = model.Status;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Status_EnableAndClosed_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Closed";
            var model = new WinLossLimitDetail { Enabled = true, Closed = true };

            // Act
            var actualValue = model.Status;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Status_EnableAndNotClosedAndSuspended_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Suspended";
            var model = new WinLossLimitDetail { Enabled = true, Closed = false, Suspended = true };

            // Act
            var actualValue = model.Status;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Status_EnableAndNotClosedAndNotSuspended_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Open";
            var model = new WinLossLimitDetail { Enabled = true, Closed = false, Suspended = false };

            // Act
            var actualValue = model.Status;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void RemainingBuyInAmount_NullMaxLoss_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail { MaxLossLimit = null };

            // Act
            var actualValue = model.RemainingBuyInAmount;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void RemainingBuyInAmount_MaxLossHasValue_ReturnNull()
        {
            // Arrange
            var expectedValue = -40;
            var model = new WinLossLimitDetail
            {
                MaxLossLimit = 10,
                DebitBalance = 30,
                CreditBalance = 20,
                Outstanding = 40
            };

            // Act
            var actualValue = model.RemainingBuyInAmount;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void LossPercent_NullMaxLoss_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail { MaxLossLimit = null };

            // Act
            var actualValue = model.LossPercent;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void LossPercent_ZeroMaxLoss_ReturnNull()
        {
            // Arrange
            var model = new WinLossLimitDetail { MaxLossLimit = 0 };

            // Act
            var actualValue = model.LossPercent;

            // Assert
            Assert.Null(actualValue);
        }

        [Fact]
        public void LossPercent_MaxLossHaveValue_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = 100;
            var model = new WinLossLimitDetail
            {
                MaxLossLimit = 10,
                DebitBalance = 30,
                CreditBalance = 20,
                Outstanding = 40
            };

            // Act
            var actualValue = model.LossPercent;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

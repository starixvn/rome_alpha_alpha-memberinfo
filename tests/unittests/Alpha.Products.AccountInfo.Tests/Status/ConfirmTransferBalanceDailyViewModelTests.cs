namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmTransferBalanceDailyViewModel")]
    public class ConfirmTransferBalanceDailyViewModelTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Checked_ReturnSearchAccount_IsAllDayBalTransfer(bool isAllDayBalTransfer)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllDayBalTransfer", isAllDayBalTransfer);

            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.Checked;

            // Assert
            Assert.Equal(isAllDayBalTransfer, act);
        }

        [Fact]
        public void SuffixMessage_Always_IsMessage()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.SuffixMessage;

            // Assert
            Assert.Equal("Message", act);
        }

        [Fact]
        public void ConfirmMessage_Always_CorrectFormat()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.Username", "razor");
            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.ConfirmMessage;

            // Assert
            Assert.Equal("ConfirmTransferBalanceDailyViewModelMessage", act);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Visible_SearchAccountIsMaster_Return_AllowedAllDayBalTransfer(bool allowedAllDayBalTransfer)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.AllowedAllDayBalTransfer", allowedAllDayBalTransfer);

            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.Visible;

            // Assert
            Assert.Equal(allowedAllDayBalTransfer, act);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(1, false)]
        public void Visible_SearchAccountIsNotMaster_Return_False(byte searchAccountRole, bool allowedAllDayBalTransfer)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, searchAccountRole);
            request.AddData("SearchAccount.AllowedAllDayBalTransfer", allowedAllDayBalTransfer);

            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.Visible;

            // Assert
            Assert.False(act);
        }

        [Fact]
        public void Url_Always_Return_CorrectUrl()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.Url;

            // Assert
            Assert.Equal("UpdateAllDayBalanceTransfer", act);
        }

        [Fact]
        public void Tooltip_Always_Return_CorrectTooltip()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Act
            var act = viewModel.Tooltip;

            // Assert
            Assert.Equal("ConfirmTransferBalanceDailyViewModelTooltip", act);
        }

        [Fact]
        public void Always_Return_CorrecttatusName()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            // Assert
            Assert.Equal("IsAllDayBalTransfer", viewModel.StatusName);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_CorrectStatusValue_By_Checked(bool isAllDayBalTransfer, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllDayBalTransfer", isAllDayBalTransfer);

            var viewModel = new ConfirmTransferBalanceDailyViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }
    }
}
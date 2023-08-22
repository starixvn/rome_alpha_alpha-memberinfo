namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmAllowAutoTransferViewModel")]
    public class ConfirmAllowAutoTransferViewModelTests
    {
        [Fact]
        public void Url_Always_ReturnCorrectValue()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);
            var expectedValue = "site-transfer/autotransfer/update";

            // Act
            var actualValue = viewModel.Url;

            // Assert
            Assert.EndsWith(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Checked_Aways_ReturnsAllowAutoTransfer(bool isAllowAutoTransfer, bool expectedValue)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.Checked;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(true, "InCaseDisabledMemberMessage")]
        [InlineData(false, "InCaseEnabledMemberMessage")]
        public void SuffixMessage_Always_ReturnCorrectValue(bool isAllowAutoTransfer, string expectedSuffixMessages)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.SuffixMessage;

            // Assert
            Assert.Equal(expectedSuffixMessages, actualValue);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void StatusValue_Always_ReturnsCorrectValue(bool isAllowAutoTransfer, bool expectedValue)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.StatusValue;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }


        [Theory]
        [InlineData(false, "ConfirmAllowAutoTransferViewModelInCaseEnabledMemberMessage")]
        [InlineData(true, "ConfirmAllowAutoTransferViewModelInCaseDisabledMemberMessage")]
        public void ConfirmMessage_Return_CorrectMessage(bool isAllowAutoTransfer, string expectedValue)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.ConfirmMessage;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ReloadCustomerInfo_Always_ReturnsFalse()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.ReloadCustomerInfo;

            // Assert
            Assert.False(actualValue);
        }

        [Fact]
        public void IsPopup_Always_ReturnsFalse()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.IsPopup;

            // Assert
            Assert.False(actualValue);
        }

        [Fact]
        public void StatusName_Return_CorrectName()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);
            var expectedValue = "AutoTransfer";

            // Act
            var actualValue = viewModel.StatusName;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Checked_Return_CorrectValue(bool isAllowAutoTransfer, bool expectedCheckedValue)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.Checked;

            // Assert
            Assert.Equal(expectedCheckedValue, actualValue);
        }

        [Theory]
        [InlineData(true, "off")]
        [InlineData(false, "on")]
        public void Params_Return_CorrectValue(bool isAllowAutoTransfer, string state)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);
            var expectedValue = new Dictionary<string, object>
            {
                ["CustId"] = "2",
                ["State"] = state,
                ["UseDifferentSite"] = 1,
            };

            // Act
            var actualValue = viewModel.Params;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ShowInCustomerList_Always_ReturnsTrue()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.ShowInCustomerList;

            // Assert
            Assert.True(actualValue);
        }

        [Theory]
        [InlineData(4, 3, false, true)]
        [InlineData(4, 3, true, false)]
        [InlineData(4, 2, true, true)]
        [InlineData(4, 1, true, true)]
        public void Disable_Return_CorrectValue(
            byte actorLevel,
            byte searchAccountLevel,
            bool hasUpdateAccountPermission,
            bool expectedStatus)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.Disabled;

            // Assert
            Assert.Equal(expectedStatus, actualValue);
        }

        [Fact]
        public void ReloadCustomerList_Always_ReturnsTrue()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            // Act
            var actualValue = viewModel.ReloadCustomerList;

            // Assert
            Assert.True(actualValue);
        }

        [Fact]
        public void JsonParams_Always_ReturnsTrue()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", true);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);
            var expectedValue = "{\"CustId\":\"2\",\"State\":\"off\",\"UseDifferentSite\":\"1\"}";

            // Act
            var actualValue = viewModel.JsonParams;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
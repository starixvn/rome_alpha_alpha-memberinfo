namespace Alpha.Products.AccountInfo.Tests.Status
{
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmUnlockAccessViewModel")]
    public class ConfirmUnlockAccessViewModelTests
    {
        [Theory]
        [InlineData((byte)0, true)]
        [InlineData((byte)1, false)]
        [InlineData((byte)2, false)]
        public void Disabled_Always_ReturnsExpectedValue(byte locked, bool expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Locked", locked);

            // Act
            var viewModel = new ConfirmUnlockAccessViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.Disabled);
        }

        [Theory]
        [InlineData((byte)0, false)]
        [InlineData((byte)1, true)]
        [InlineData((byte)2, true)]
        public void Checked_Always_ReturnsExpectedValue(byte locked, bool expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.Locked", locked);

            // Act
            var viewModel = new ConfirmUnlockAccessViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.Checked);
        }

        [Theory]
        [InlineData((byte)0, "InCaseNone")]
        [InlineData((byte)1, "InCaseFalseOTPSC")]
        [InlineData((byte)2, "InCaseFalsePassword")]
        public void SuffixMessage_Always_ReturnsExpectedValue(byte locked, string expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.Locked", locked);

            // Act
            var viewModel = new ConfirmUnlockAccessViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.SuffixMessage);
        }
    }
}

namespace Alpha.Products.AccountInfo.Tests.Status
{
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmProtectedViewModel")]
    public class ConfirmProtectedViewModelTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Visible_IsOfferProtectedMode_ReturnsExpectedValue(bool isOffer, bool expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsOfferProtectedMode", isOffer);

            // Act
            var viewModel = new ConfirmProtectedViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.Visible);
        }

        [Theory]
        [InlineData((byte)3, true)]
        [InlineData((byte)2, false)]
        [InlineData((byte)1, false)]
        public void Visible_Downline_ReturnsExpectedValue(byte downlineRoleId, bool expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, downlineRoleId);
            request.AddData("SearchAccount.IsOfferProtectedMode", true);

            // Act
            var viewModel = new ConfirmProtectedViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.Visible);
        }

        [Theory]
        [InlineData((byte)4, (byte)3, true)]
        [InlineData((byte)3, (byte)2, true)]
        [InlineData((byte)2, (byte)1, false)]
        public void VisibleIsNotMember_ReturnsExpectedValue(byte uplineRoleId, byte downlineRoleId, bool expected)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(uplineRoleId, downlineRoleId);
            request.AddData("SearchAccount.IsOfferProtectedMode", true);

            // Act
            var viewModel = new ConfirmProtectedViewModel(request);

            // Assert
            Assert.Equal(expected, viewModel.Visible);
        }
    }
}

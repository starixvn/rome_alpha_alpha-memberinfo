namespace Alpha.Products.AccountInfo.Tests.Setting.ViewModels.Multiple
{
    using System.Collections.Generic;
    using AccountInfo.Setting.ViewModels.Products;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.AutoTransferMultipleViewModel")]
    public class AutoTransferMultipleViewModelTests
    {
        [Fact]
        public void URL_Always_ReturnCorrectValue()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new AutoTransferMultipleViewModel(request);
            request.AddData("customerIds", "1,3");
            request.AddData("usernames", "master01");
            string expectedValue = "?SearchAccountIds=1,3&Usernames=master01&SearchAccountRoleId=3";

            // Act
            var actualValue = viewModel.Url;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Text_Always_CorrectName()
        {
            // Arrange
            var expectedText = "EditMultipleAutoTransferText";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new AutoTransferMultipleViewModel(request);

            // Act
            var actualValue = viewModel.Text;

            // Assert
            Assert.StartsWith(expectedText, actualValue);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            // Arrange
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("customerIds", "1, 3");
            request.AddData("usernames", "master01");
            var viewModel = new AutoTransferMultipleViewModel(request);
            var epectedQueryStringParamaters = new Dictionary<string, string>
            {
                ["SearchAccountIds"] = "1, 3",
                ["Usernames"] = "master01",
                ["SearchAccountRoleId"] = "3"
            };

            // Act
            var actualValue = viewModel.QueryStringParamaters;

            // Assert
            Assert.Equal(epectedQueryStringParamaters, actualValue);
        }

        [Theory]
        [InlineData(4, 2, false)]
        [InlineData(4, 3, true)]
        [InlineData(4, 1, false)]
        [InlineData(3, 2, true)]
        [InlineData(3, 1, false)]
        [InlineData(2, 1, true)]
        public void Visible_Return_CorrectValue(byte actorLevel, byte searchAccountLevel, bool expectedStatus)
        {
            // Arrange
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new AutoTransferMultipleViewModel(request);

            // Act
            var actualValue = viewModel.Visible;

            // Assert
            Assert.Equal(expectedStatus, actualValue);
        }
    }
}
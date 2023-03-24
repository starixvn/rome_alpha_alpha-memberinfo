using System.Linq;

namespace Alpha.Products.AccountInfo.Tests.Setting.ViewModels.Multiple
{
    using AccountInfo.Setting.ViewModels.Products;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.AutoTransferMultipleViewModel")]
    public class AutoTransferMultipleViewModelTests
    {
        [Fact]
        public void Url_Ends_With_AutoTransferUpdateAddress()
        {
            string autoTransferUpdateAddress = "/AutoTransfer/UpdateMultiple";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new AutoTransferMultipleViewModel(request);
            request.AddData("customerIds", "1, 3");
            request.AddData("usernames", "master01");
            var queryString = string.Join("&", viewModel.QueryStringParamaters.Select(query => $"{query.Key}={query.Value}"));

            string expectedAutoTransferUpdateAddress = $"{autoTransferUpdateAddress}?{queryString}";

            Assert.EndsWith(expectedAutoTransferUpdateAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleAutoTransferText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new AutoTransferMultipleViewModel(request);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("customerIds", "1, 3");
            request.AddData("usernames", "master01");

            var viewModel = new AutoTransferMultipleViewModel(request);

            Assert.Equal("1, 3", viewModel.QueryStringParamaters["SearchAccountIds"]);
            Assert.Equal("master01", viewModel.QueryStringParamaters["Usernames"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["SearchAccountRoleId"]);
        }

        [Theory]
        [InlineData(4, 2, false)]
        [InlineData(4, 3, true)]
        [InlineData(4, 1, false)]
        [InlineData(3, 2, true)]
        [InlineData(3, 1, false)]
        [InlineData(2, 1, true)]
        public void Visible_Return_CorrectValue(
          byte actorLevel,
          byte searchAccountLevel,
          bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);

            var viewModel = new AutoTransferMultipleViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Visible);
        }
    }
}
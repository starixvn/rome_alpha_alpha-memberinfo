namespace Alpha.Products.AccountInfo.Tests.Setting.ViewModels.Multiple
{
    using AccountInfo.Setting.ViewModels.Products;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.ClosedMultipleViewModel")]
    public class ClosedMultipleViewModelTests
    {
        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            string expectedBaseAddress = "MicroSiteName/OpenDownlines/EditMultiple";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ClosedMultipleViewModel(request);

            Assert.StartsWith(expectedBaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleClosedStatusText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ClosedMultipleViewModel(request);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("customerIds", "1, 3");

            var viewModel = new ClosedMultipleViewModel(request);

            Assert.Equal("1, 3", viewModel.QueryStringParamaters["CustIds"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["RoleId"]);
        }

        [Theory]
        [InlineData(4, 2, false)]
        [InlineData(4, 3, true)]
        [InlineData(4, 1, true)]
        [InlineData(3, 2, true)]
        [InlineData(3, 1, true)]
        [InlineData(2, 1, true)]
        public void Visible_Return_CorrectValue(
          byte actorLevel,
          byte searchAccountLevel,
          bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);

            var viewModel = new ClosedMultipleViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Visible);
        }
    }
}
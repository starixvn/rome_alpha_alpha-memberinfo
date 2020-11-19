namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.SuspendedMultipleViewModel")]
    public class SuspendedMultipleViewModelTests
    {
        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            string expectedBaseAddress = "MicroSiteName/UnsuspendDownlines/EditMultiple";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SuspendedMultipleViewModel(request);

            Assert.StartsWith(expectedBaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleSuspendedStatusText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new SuspendedMultipleViewModel(request);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("customerIds", "1, 3");

            var viewModel = new SuspendedMultipleViewModel(request);

            Assert.Equal("1, 3", viewModel.QueryStringParamaters["CustIds"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["RoleId"]);
        }
    }
}
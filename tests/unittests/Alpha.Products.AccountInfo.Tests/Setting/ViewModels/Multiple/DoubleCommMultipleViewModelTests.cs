namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.DoubleCommMultipleViewModel")]
    public class DoubleCommMultipleViewModelTests
    {
        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            string expectedBaseAddress = "MicroSiteName/_AccountInfo/ComfirmMultiEditDoubleComm.aspx";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DoubleCommMultipleViewModel(request);

            Assert.StartsWith(expectedBaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleDoubleCommText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new DoubleCommMultipleViewModel(request);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 1);
            request.AddData("customerIds", "1, 3");

            var viewModel = new DoubleCommMultipleViewModel(request);

            Assert.Equal("1, 3", viewModel.QueryStringParamatersMultiple["arCID"]);
        }
    }
}
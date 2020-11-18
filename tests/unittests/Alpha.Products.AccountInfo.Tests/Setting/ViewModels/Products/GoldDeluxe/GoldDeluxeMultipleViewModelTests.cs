namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.GoldDeluxeMultipleViewModel")]
    public class GoldDeluxeMultipleViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/GoldDeluxeSetting/GoldDeluxe/EditSettingMultiple";
        private readonly IConfigurationItem ProductItem;

        public GoldDeluxeMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(12);
        }

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeMultipleViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Icon_With_CorrectNames()
        {
            var expectedIcon = "icon-edit Enable";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedIcon, viewModel.Icon, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleGoldDeluxeText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new GoldDeluxeMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("customerIds", "1, 3");
            request.AddData("usernames", "test1, test2");

            var viewModel = new GoldDeluxeMultipleViewModel(request, ProductItem);

            Assert.Equal("0", viewModel.QueryStringParamaters["isForcingMode"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["isGetMultipleDCS"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["formType"]);
            Assert.Equal("0", viewModel.QueryStringParamaters["subAccId"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["level"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["customerLevel"]);
            Assert.Equal("test1, test2", viewModel.QueryStringParamaters["usernames"]);
            Assert.Equal("1, 3", viewModel.QueryStringParamaters["customerIds"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["actorId"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["uplineId"]);
            Assert.Equal("4", viewModel.QueryStringParamaters["uplineLevel"]);
        }
    }
}
namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.DynamicProductMultipleViewModel")]
    public class DynamicProductMultipleViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/dcs/viewcustomersetting";
        private readonly IConfigurationItem ProductItem;

        public DynamicProductMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(21);
        }

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DynamicProductMultipleViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Icon_With_CorrectNames()
        {
            var expectedIcon = "icon-edit Enable";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DynamicProductMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedIcon, viewModel.Icon, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultiple ProductName-21";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new DynamicProductMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 1);
            request.AddData("customerIds", "1, 3");
            request.AddData("usernames", "test1, test2");
            request.AddData("language", "EN");

            var viewModel = new DynamicProductMultipleViewModel(request, ProductItem);

            Assert.Equal("1", viewModel.QueryStringParamaters["isGetMultipleDCS"]);
            Assert.Equal("true", viewModel.QueryStringParamaters["isMultiple"]);
            Assert.Equal("EN", viewModel.QueryStringParamaters["language"]);
            Assert.Equal("false", viewModel.QueryStringParamaters["isForcingMode"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["formType"]);
            Assert.Equal("1, 3", viewModel.QueryStringParamaters["customerIds"]);
            Assert.Equal("test1, test2", viewModel.QueryStringParamaters["usernames"]);
            Assert.Equal("test1, test2", viewModel.QueryStringParamaters["customerNames"]);
            Assert.Equal("0", viewModel.QueryStringParamaters["subAccId"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["customerLevel"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["actorId"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["uplineId"]);
            Assert.Equal("21", viewModel.QueryStringParamaters["productId"]);
            Assert.Equal("4", viewModel.QueryStringParamaters["uplinelevel"]);
        }
    }
}
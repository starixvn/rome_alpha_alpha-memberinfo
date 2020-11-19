namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.KenoMultipleViewModel")]
    public class KenoMultipleViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public KenoMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(8);
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteName/customersetting/kenosetting/kenosetting/EditMaster")]
        [InlineData(4, 2, "MicroSiteName/customersetting/kenosetting/kenosetting/EditAgent")]
        [InlineData(4, 1, "MicroSiteName/customersetting/kenosetting/kenosetting/SuperEditMember")]
        public void Url_Start_With_BaseAddress(byte actorRoleId, byte searchAccountRoleId, string expectedBaseAddess)
        {
            var request = RequestHelper.CreateRequest(actorRoleId, searchAccountRoleId);
            var viewModel = new KenoMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedBaseAddess, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Icon_With_CorrectNames()
        {
            var expectedIcon = "icon-edit Enable";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedIcon, viewModel.Icon, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleRNGKenoText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new KenoMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new KenoMultipleViewModel(request, ProductItem);

            Assert.Equal("1", viewModel.QueryStringParamaters["isMultipleEdit"]);
            Assert.Equal("0", viewModel.QueryStringParamaters["SubAccId"]);
            Assert.Equal("1", viewModel.QueryStringParamaters["userId"]);
            Assert.Equal("2", viewModel.QueryStringParamaters["custId"]);
            Assert.Equal("username1", viewModel.QueryStringParamaters["username"]);
        }
    }
}
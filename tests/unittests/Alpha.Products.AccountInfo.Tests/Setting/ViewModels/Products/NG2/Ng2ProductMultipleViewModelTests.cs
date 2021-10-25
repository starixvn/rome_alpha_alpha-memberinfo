namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.Ng2ProductViewModel")]
    public class Ng2ProductMultipleViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public Ng2ProductMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(23);
        }

        [Fact]
        public void Icon_Return_CorrectClasses()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData(DataKey.Theme.IsDarkMode, false);
            var viewModel = new Ng2ProductMultipleViewModel(request, ProductItem);

            Assert.Equal("icon-edit Enable", viewModel.Icon);
        }

        [Fact]
        public void Text_Return_CorrectName()
        {
            var expectedText = "EditMultiple ProductName-23";
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData(DataKey.Theme.IsDarkMode, false);
            var viewModel = new Ng2ProductMultipleViewModel(request, ProductItem);

            Assert.Equal(expectedText, viewModel.Text);
        }
    }
}
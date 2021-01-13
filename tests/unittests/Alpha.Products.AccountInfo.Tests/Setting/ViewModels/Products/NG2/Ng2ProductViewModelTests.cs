namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.Ng2ProductViewModel")]
    public class Ng2ProductViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/dcs/viewcustomersetting";
        private readonly IConfigurationItem ProductItem;

        public Ng2ProductViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(23);
        }

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new Ng2ProductViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }
    }
}
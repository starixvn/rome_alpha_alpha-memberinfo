namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.RnGCasinoMultipleViewModel")]
    public class RnGCasinoMultipleViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public RnGCasinoMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(6);
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteName/CustomerSetting/Casino/Master/EditMaster")]
        [InlineData(4, 1, "MicroSiteName/CustomerSetting/Casino/Member/EditMemberBySuper")]
        [InlineData(3, 2, "MicroSiteName/CustomerSetting/Casino/Agent/EditAgent")]
        [InlineData(3, 1, "MicroSiteName/CustomerSetting/Casino/Member/EditMemberByMaster")]
        [InlineData(2, 1, "MicroSiteName/CustomerSetting/Casino/Member/EditMember")]
        public void Url_Start_With_BaseAddress(byte actorLevel, byte searchAccountLevel, string baseAddress)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("syncedCasinoList", "1,0,1");
            var viewModel = new RnGCasinoMultipleViewModel(request, ProductItem);

            Assert.True(viewModel.Url.StartsWith(baseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Icon_With_CorrectNames()
        {
            var expectedIcon = "icon-edit Enable";
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("syncedCasinoList", "1,0,1");
            var viewModel = new RnGCasinoMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedIcon, viewModel.Icon, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleCasinoText";
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("syncedCasinoList", "1,0,1");
            var viewModel = new RnGCasinoMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
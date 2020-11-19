namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.SportsbookMultipleViewModel")]
    public class SportsbookMultipleViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public SportsbookMultipleViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(1);
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteName/_MemberInfo/PositionTaking/Super/EditMultipleMasters.aspx")]
        [InlineData(4, 1, "MicroSiteName/_MemberInfo/PositionTaking/Super/EditMultipleMembers.aspx")]
        [InlineData(3, 2, "MicroSiteName/_MemberInfo/PositionTaking/Master/EditMultipleAgents.aspx")]
        [InlineData(3, 1, "MicroSiteName/_MemberInfo/PositionTaking/Master/EditMultipleMembers.aspx")]
        [InlineData(2, 1, "MicroSiteName/_MemberInfo/PositionTaking/Agent/EditMultipleMembers.aspx")]
        public void Url_Start_With_BaseAddress(byte actorLevel, byte searchAccountLevel, string baseAddress)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new SportsbookMultipleViewModel(request, ProductItem);

            Assert.True(viewModel.Url.StartsWith(baseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Icon_With_CorrectNames()
        {
            var expectedIcon = "icon-edit Enable";
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedIcon, viewModel.Icon, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var expectedText = "EditMultipleSportsbookText";
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new SportsbookMultipleViewModel(request, ProductItem);

            Assert.StartsWith(expectedText, viewModel.Text, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void QueryStringParamaters_With_Correctvalues()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new SportsbookMultipleViewModel(request, ProductItem);

            Assert.Equal("1", viewModel.QueryStringParamaters["m"]);
            Assert.Equal("2", viewModel.QueryStringParamaters["custId"]);
            Assert.Equal("2", viewModel.QueryStringParamaters["Superid"]);
            Assert.Equal("username1", viewModel.QueryStringParamaters["user"]);
        }
    }
}
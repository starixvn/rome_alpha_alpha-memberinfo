namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.SportsbookViewModel")]
    public class SportsbookViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public SportsbookViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(1);
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteName/_MemberInfo/PositionTaking/Super/EditMaster.aspx")]
        [InlineData(4, 1, "MicroSiteName/_MemberInfo/PositionTaking/Super/EditMember.aspx")]
        [InlineData(3, 2, "MicroSiteName/_MemberInfo/PositionTaking/Master/EditAgent.aspx")]
        [InlineData(3, 1, "MicroSiteName/_MemberInfo/PositionTaking/Master/EditMember.aspx")]
        [InlineData(2, 1, "MicroSiteName/_MemberInfo/PositionTaking/Agent/EditMember.aspx")]
        public void Url_Start_With_BaseAddress(byte actorLevel, byte searchAccountLevel, string baseAddress)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Url.StartsWith(baseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Url_QueryString_User_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"user={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"custId={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(4, 3, "superid")]
        [InlineData(4, 1, "agentid")]
        [InlineData(3, 2, "masterid")]
        [InlineData(3, 1, "agentid")]
        [InlineData(2, 1, "agentid")]
        public void Url_QueryString_Returns_CustId_Of_SearchAccount_Upline(byte actorLevel, byte searchAccountLevel, string uplineIdParam)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"{uplineIdParam}={request.SearchAccount.Upline.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_M_Returns_Multiple_Param_Is_False()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf("m=0", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Icon_Return_CorrectClass()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.Equal("icon-edit Enable", viewModel.Icon);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.Equal("ProductName-1", viewModel.Text, ignoreCase: true);
        }

        [Fact]
        public void Enable_Always_true()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookViewModel(request, ProductItem);

            Assert.True(viewModel.Enabled);
        }
    }
}
namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using Core.Models;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.GoldDeluxeViewModel")]
    public class GoldDeluxeViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/GoldDeluxeSetting/GoldDeluxe/EditSetting";
        private readonly IConfigurationItem ProductItem;

        public GoldDeluxeViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(12);
        }

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_UplineId_Returns_UplineId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"uplineId={request.Actor.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Url_QueryString_UplineLevel_Returns_UplineLevel_Of_SearchAccount(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"uplineLevel={request.Actor.CusomerLevelValue}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_ActorId_Returns_CustId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"actorId={request.Actor.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"custId={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Url_QueryString_Level_Returns_SearchAccount_Level(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"level={request.SearchAccount.CusomerLevelValue}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_SubAccId_Returns_SubAccId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"subAccId={request.Actor.SubAccountId}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Contains_FormType_Param()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf("formType=", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Visibleness_By_Actor_GoldDeluxe_Hidden_Property(bool hidden, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(12, hidden: hidden)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorIsNotSuperOrSearchAccountIsNotAgentLevelTestData))]
        public void Visible_Is_True_When_Actor_Is_Not_Super_Or_SeachAccount_Is_Not_Agent(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            // Assert
            Assert.True(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_False_When_Actor_Is_Super_And_SeachAccount_Is_Agent()
        {
            //Arrange
            var actorAccount = CustomerHelper.CreateCustomer(1, 4);
            var searchAccount = CustomerHelper.CreateCustomer(2, 2);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new GoldDeluxeViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }
    }
}
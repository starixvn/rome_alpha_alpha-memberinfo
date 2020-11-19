namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using Core.Models;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.RNGCasinoViewModel")]
    public class RNGCasinoViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public RNGCasinoViewModelTests()
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
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.StartsWith(baseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
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
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

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
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_False_When_Of_Actor_Is_Not_Synced_RNGCasino()
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: false)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Visibleness_By_Actor_RnGCasino_Synced_Property_When_Actor_Edit_DirectDownline(bool synced, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: synced)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Edit_CrossDownlines_And_Actor_Is_Not_Synced_RNGCasino(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: false)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Edit_CrossDownlines_And_SearchAccount_Is_Not_Synced_RNGCasino(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var actorProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: true)
            };

            var searchAccountProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: false)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: actorProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel, customerProductStatus: searchAccountProductStatus);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(4, 1)]
        [InlineData(3, 1)]
        public void Visible_Is_True_When_Synced_RNGCasino_Of_Actor_Is_True_And_Synced_SearchAccount_Is_True(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var actorProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: true)
            };

            var searchAccountProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: true)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: actorProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel, customerProductStatus: searchAccountProductStatus);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new RnGCasinoViewModel(request, ProductItem);

            // Assert
            Assert.True(viewModel.Visible);
        }
    }
}
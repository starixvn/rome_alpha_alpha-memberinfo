namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Configuration;
    using Core.Models;
    using NSubstitute;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels.Base", "ProductStatusItemViewModel")]
    public class ProductStatusItemViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/ProductStatus?";

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_Returns_CustId_Of_SearchAccount()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Url.IndexOf($"custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Returns_ProductId()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            const int productId = 15;
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(productId);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Url.IndexOf($"productid={productId}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Returns_SearchAccount_Username()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Url.IndexOf($"custName={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Url_QueryString_Returns_SearchAccount_RoleLevel(byte actorLevel, byte searchAccountLevel)
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Url.IndexOf($"&custRoleId={request.SearchAccount.CusomerLevelValue}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Contains_UID_Paramater()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Url.IndexOf("uid=", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Disabled_Is_True_When_SearchAccount_Is_Not_DirectDownline_Of_Actor(byte actorLevel, byte searchAccountLevel)
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(15);
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            var viewModel = new ProductStatusItemViewModel(request, configItem);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [ClassData(typeof(ActorAndDirectDownlineSearchAccountLevelTestData))]
        public void Visible_Is_True_When_Actor_Edit_DirectDownline_And_SearchAccount_CanSeeProduct(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: false)
            };

            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(21);
            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            // Act
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            // Assert
            Assert.True(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_SearchAccount_Is_Not_DirectDownline_Of_Actor(byte actorLevel, byte searchAccountLevel)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: false)
            };

            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(21);
            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            // Act
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_False_When_SearchAccount_Can_Not_See_Product()
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: true)
            };

            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(21);
            var actorAccount = CustomerHelper.CreateCustomer(1, 4);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3, customerProductStatus: customerProductStatus);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            // Act
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Checked_By_SearchAccount_Product_Enabled_Status(bool disabled, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, disabled: disabled)
            };

            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(21);
            var actorAccount = CustomerHelper.CreateCustomer(1, 4);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3, customerProductStatus: customerProductStatus);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            // Act
            var viewModel = new ProductStatusItemViewModel(request, configItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Checked);
        }
    }
}
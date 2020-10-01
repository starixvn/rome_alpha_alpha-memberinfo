namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using Core.Models;
    using NSubstitute;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "Product.DynamicProductViewModel")]
    public class DynamicProductViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/dcs/viewcustomersetting";
        private readonly IConfigurationItem ProductItem;

        public DynamicProductViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(21);
        }

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"customerId={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_CustomerLevel_Returns_CustLevel_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"?customerLevel={request.SearchAccount.CustomerLevel.Value}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        public void Url_QueryString_Uplinelevel_Return_CustomerLevel_Of_Actor_When_SearchAccount_Is_Member(byte actorLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, 1);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"uplinelevel={request.Actor.CustomerLevel.Value}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(4, 2)]
        [InlineData(3, 2)]
        public void Url_QueryString_Does_Not_Contains_Uplinelevel_Paramater_When_SearchAccount_Is_Not_Member(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"uplinelevel={request.Actor.CustomerLevel.Value}", StringComparison.OrdinalIgnoreCase).Equals(-1));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Visibleness_By_Actor_Product_Hidden_Property(bool hidden, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: hidden)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Visibleness_By_Actor_Product_Synced_Property(bool synced, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, synced: synced)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }
    }
}
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
            request.AddData(DataKey.Theme.IsDarkMode, true);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData(DataKey.Theme.IsDarkMode, true);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"customerId={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_CustomerLevel_Returns_CustLevel_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData(DataKey.Theme.IsDarkMode, true);
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
            request.AddData(DataKey.Theme.IsDarkMode, true);
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
            request.AddData(DataKey.Theme.IsDarkMode, true);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"uplinelevel={request.Actor.CustomerLevel.Value}", StringComparison.OrdinalIgnoreCase).Equals(-1));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Url_QueryString_IsDarkMode_Return_Correct_Value(bool isDarkMode)
        {
            var request = RequestHelper.CreateRequest(4, 1);
            request.AddData(DataKey.Theme.IsDarkMode, isDarkMode);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"isDarkMode={isDarkMode}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData(DataKey.Theme.IsDarkMode, true);
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            Assert.Equal("ProductName-21", viewModel.Text, ignoreCase: true);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(false, false)]
        public void Visible_Return_CorrectValueByActorStatus(
        bool hidden,
        bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: hidden)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData(DataKey.Theme.IsDarkMode, true);

            // Act
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }

        [Theory]
        [InlineData(4, 3, true)]
        [InlineData(4, 2, false)]
        [InlineData(4, 1, true)]
        [InlineData(3, 2, true)]
        [InlineData(3, 1, true)]
        [InlineData(2, 1, true)]
        public void Visible_Return_CorrectValueByLevel(
           byte actorLevel,
           byte searchAccountLevel,
           bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(21, hidden: false)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, actorLevel, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            request.AddData(DataKey.Theme.IsDarkMode, true);

            // Act
            var viewModel = new DynamicProductViewModel(request, ProductItem);

            // Assert
            Assert.Equal(expectedValue, viewModel.Visible);
        }
    }
}
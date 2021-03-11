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

    [Trait("Setting.ViewModels", "Product.KenoViewModel")]
    public class KenoViewModelTests
    {
        private readonly IConfigurationItem ProductItem;

        public KenoViewModelTests()
        {
            ProductItem = Substitute.For<IConfigurationItem>();
            ProductItem.ProductId.Returns(8);
        }

        [Theory]
        [InlineData(4, 3, "MicroSiteName/customersetting/kenosetting/kenosetting/EditMaster")]
        [InlineData(4, 1, "MicroSiteName/customersetting/kenosetting/kenosetting/SuperEditMember")]
        [InlineData(3, 2, "MicroSiteName/customersetting/kenosetting/kenosetting/EditAgent")]
        [InlineData(3, 1, "MicroSiteName/customersetting/kenosetting/kenosetting/MasterEditMember")]
        [InlineData(2, 1, "MicroSiteName/customersetting/kenosetting/kenosetting/AgentEditMember")]
        public void Url_Start_With_BaseAddress(byte actorLevel, byte searchAccountLevel, string baseAddress)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.StartsWith(baseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"custId={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_UserId_Return_CustId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"userId={request.Actor.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_SubAccId_Return_SubAccId_Of_Actor()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.True(viewModel.Url.IndexOf($"SubAccId={request.Actor.SubAccountId}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_Visibleness_By_Actor_Keno_Hidden_Property(bool hidden, bool expectedValue)
        {
            //Arrange
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(8, hidden: hidden)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4, customerProductStatus: customerProductStatus);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);

            // Act
            var viewModel = new KenoViewModel(request, ProductItem);

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
            var viewModel = new KenoViewModel(request, ProductItem);

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
            var viewModel = new KenoViewModel(request, ProductItem);

            // Assert
            Assert.False(viewModel.Visible);
        }

        [Fact]
        public void Text_With_CorrectName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new KenoViewModel(request, ProductItem);

            Assert.Equal("ProductName-8", viewModel.Text, ignoreCase: true);
        }
    }
}
namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Alpha.Products.Core.Models;
    using System;
    using System.Collections.Generic;
    using Xunit;

    [Trait("Setting.ViewModels", "ResetPasswordViewModel")]
    public class ResetPasswordViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/Password/ResetMainAccountPassword";

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetPasswordViewModel(request);

            Assert.True(viewModel.Url.StartsWith(BaseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetPasswordViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetPasswordViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Url_QueryString_CustRoleId_Returns_Customer_Level_Of_SearchAccount(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new ResetPasswordViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&custRoleId={request.SearchAccount.CusomerLevelValue}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Visible_Is_True_When_Actor_Is_MainAccount(byte actorLevel, byte searchAccountLevel)
        {
            var actorMainAccount = CustomerHelper.CreateCustomer(1, actorLevel, subAccountId: 0);
            var searchAccount = CustomerHelper.CreateCustomer(1, searchAccountLevel);
            var request = RequestHelper.CreateRequestFromCustomers(actorMainAccount, searchAccount);

            var model = new ResetPasswordViewModel(request);

            Assert.True(model.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Visible_Is_True_When_Actor_Has_UpdateAccountPermission(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);

            var model = new ResetPasswordViewModel(request);

            Assert.True(model.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Does_Not_Have_UpdateAccountPermission(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(
                actorLevel,
                actorIsMainAccount: false,
                searchAccountLevel: searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", false);

            var model = new ResetPasswordViewModel(request);

            Assert.False(model.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndSearchAccountLevelTestData))]
        public void Text_Is_ReturnedFromResourceGetter(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);

            var viewModel = new ResetPasswordViewModel(request);

            Assert.Equal($"{typeof(ResetPasswordViewModel).Name}Text", viewModel.Text);
        }

        [Fact]
        public void AddEditTextToPopupTitle_Always_False()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var model = new ResetPasswordViewModel(request);

            Assert.False(model.AddEditTextToPopupTitle);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void QueryStringParamaters_With_Correctvalues(bool searchAccountSynced)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(6, synced: searchAccountSynced)
            };

            var actorAccount = CustomerHelper.CreateCustomer(1, 4);
            var searchAccount = CustomerHelper.CreateCustomer(2, 3, customerProductStatus: customerProductStatus);
            var request = RequestHelper.CreateRequestFromCustomers(actorAccount, searchAccount);
            var viewModel = new ResetPasswordViewModel(request);

            Assert.Equal("2", viewModel.QueryStringParamaters["custId"]);
            Assert.Equal("username1", viewModel.QueryStringParamaters["username"]);
            Assert.Equal("3", viewModel.QueryStringParamaters["custRoleId"]);           
        }
    }
}
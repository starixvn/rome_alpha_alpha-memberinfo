namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels;
    using Alpha.Products.Core.Models;
    using System;
    using System.Collections.Generic;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "InfoViewModel")]
    public class EditInfoViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/_AccountInfo/EditCustomerInfo.aspx";

        [Fact]
        public void Url_Starts_With_BaseAddress()
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(4);
            var viewModel = new EditInfoViewModel(request);

            Assert.True(viewModel.Url.StartsWith(BaseAddress, StringComparison.OrdinalIgnoreCase) == true);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(4);
            var viewModel = new EditInfoViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorLevelTestData))]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount(byte actorLevel)
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(actorLevel);
            var viewModel = new EditInfoViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorLevelTestData))]
        public void Visible_Is_True_When_Actor_Edit_Direct_Downline(byte actorLevel)
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(actorLevel);
            var viewModel = new EditInfoViewModel(request);

            Assert.Equal(viewModel.Visible, request.ActorIsEdittingDirectDownline);
        }

        [Theory]
        [ClassData(typeof(ActorLevelTestData))]
        public void Text_Is_Returned_From_GetStringFunction(byte actorLevel)
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(actorLevel);

            var viewModel = new EditInfoViewModel(request);

            Assert.Equal($"{typeof(EditInfoViewModel).Name}Text", viewModel.Text);
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
            var viewModel = new EditInfoViewModel(request);

            Assert.Equal("2", viewModel.QueryStringParamaters["custId"]);
            Assert.Equal("username1", viewModel.QueryStringParamaters["username"]);
        }
    }
}
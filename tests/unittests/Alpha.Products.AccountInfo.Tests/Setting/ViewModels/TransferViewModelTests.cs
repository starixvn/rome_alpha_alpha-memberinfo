namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using AccountInfo.Setting.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "TransferViewModel")]
    public class TransferViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/_MemberInfo/TransferCondition/EditTransferCondition.aspx";

        [Fact]
        public void Url_Starts_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new TransferViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_Contains_CustId_Paramater()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new TransferViewModel(request);

            Assert.True(viewModel.Url.IndexOf("?custid=", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new TransferViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new TransferViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Super_And_SearchAccount_Is_Master()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new TransferViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Master_And_SearchAccount_Is_Agent_And_Have_CanEditDailyTransferOption()
        {
            var request = RequestHelper.CreateRequest(3, 2);
            request.AddData("Actor.CanEditDailyTransferOption", true);

            var viewModel = new TransferViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(4, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 1)]
        [InlineData(2, 1)]
        public void Visible_Is_False_When_Actor_Have_Not_CanEditDailyTransferOption(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.CanEditDailyTransferOption", false);

            var viewModel = new TransferViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Is_Not_Direct_Upline_Of_Search_Account(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new TransferViewModel(request);

            Assert.False(viewModel.Visible);
        }
    }
}
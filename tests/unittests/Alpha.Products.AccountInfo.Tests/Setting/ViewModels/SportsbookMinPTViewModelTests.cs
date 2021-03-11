namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using AccountInfo.Setting.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "SportsbookMinPTViewModel")]
    public class SportsbookMinPTViewModelTests
    {
        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Super_And_SearchAccount_Is_Master()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Master_And_SearchAccount_Is_Agent()
        {
            var request = RequestHelper.CreateRequest(3, 2);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Fact]
        public void Visible_Is_False_When_Actor_Is_Agent_And_SearchAccount_Is_Member()
        {
            var request = RequestHelper.CreateRequest(2, 1);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Is_Not_Direct_Upline_Of_Search_Account(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new SportsbookMinPTViewModel(request);

            Assert.False(viewModel.Visible);
        }
    }
}
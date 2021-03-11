namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using AccountInfo.Setting.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "CommissionViewModel")]
    public class CommissionViewModelTests
    {
        private const string EditCommisionUrl = "MicroSiteName/_MemberInfo/EditCommission/EditCommission.aspx";
        private const string EditMemberCommisionUrl = "MicroSiteName/_MemberInfo/EditCommission/EditMemberCommission.aspx";

        [Fact]
        public void Url_Starts_With_EditCommisionUrl_When_Search_Account_Is_Not_Member()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new CommissionViewModel(request);

            Assert.StartsWith(EditCommisionUrl, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_Starts_With_EditMemberCommisionUrl_When_Agent_Edit_Member()
        {
            var request = RequestHelper.CreateRequest(2, 1);
            var viewModel = new CommissionViewModel(request);

            Assert.StartsWith(EditMemberCommisionUrl, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new CommissionViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new CommissionViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [ClassData(typeof(ActorAndDirectDownlineSearchAccountLevelTestData))]
        public void Visible_Is_True_When_Actor_Is_Direct_Upline_Of_Search_Account(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new CommissionViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Is_Not_Direct_Upline_Of_Search_Account(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new CommissionViewModel(request);

            Assert.False(viewModel.Visible);
        }
    }
}
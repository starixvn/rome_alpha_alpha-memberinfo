namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "ResetSecurityCodeViewModel")]
    public class ResetSecurityCodeViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/SecurityCode/ResetSecurityCode";

        [Fact]
        public void Url_Starts_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        public void Visible_Is_False_When_SearchAccount_Is_Member(byte actorLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, 1);
            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Is_Not_Direct_Upline_Of_Search_Account_And_Actor_Is_Agent(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        public void Visible_Is_False_When_Actor_Does_Not_Have_UpdateAccountPermission(byte actorLevel)
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(actorLevel, actorIsMainAccount: false);
            request.AddData("Actor.HasUpdateAccountPermission", false);

            var model = new ResetSecurityCodeViewModel(request);

            Assert.False(model.Visible);
        }

        [Fact]
        public void AddEditTextToPopupTitle_Always_False()
        {
            var request = RequestHelper.CreateActorEditDirectDownlineRequest(4, actorIsMainAccount: false);

            var model = new ResetSecurityCodeViewModel(request);

            Assert.False(model.AddEditTextToPopupTitle);
        }

        [Theory]
        [InlineData(3, false, true, false)]
        [InlineData(3, true, false, false)]
        [InlineData(2, false, true, false)]
        [InlineData(1, false, true, false)]
        [InlineData(3, true, true, true)]
        public void Visible_Return_CorrectValue(
            byte searchAccountLevel,
            bool hasUpdateAccountPermission,
            bool searchAccountIsEnableSecurityCode,
            bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(4, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);
            request.AddData("SearchAccount.IsEnableSecurityCode", searchAccountIsEnableSecurityCode);

            var viewModel = new ResetSecurityCodeViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Visible);
        }
    }
}
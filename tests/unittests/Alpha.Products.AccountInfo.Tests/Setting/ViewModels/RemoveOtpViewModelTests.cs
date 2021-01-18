namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using AccountInfo.Setting.ViewModels;
    using System;
    using Utility;
    using Xunit;

    [Trait("Setting.ViewModels", "RemoveOtpViewModel")]
    public class RemoveOtpViewModelTests
    {
        private const string BaseAddress = "MicroSiteName/Otp/ResetOtp";

        [Fact]
        public void Url_Starts_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new RemoveOtpViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Url_QueryString_CustId_Returns_CustId_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new RemoveOtpViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"?custid={request.SearchAccount.Id}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Fact]
        public void Url_QueryString_Username_Returns_Username_Of_SearchAccount()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new RemoveOtpViewModel(request);

            Assert.True(viewModel.Url.IndexOf($"&username={request.SearchAccount.Username}", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        public void Visible_Is_False_When_SearchAccount_Is_Member(byte actorLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, 1);
            var viewModel = new RemoveOtpViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Visible_Is_False_When_Actor_Is_Not_Direct_Upline_Of_Search_Account_And_Actor_Is_Agent(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new RemoveOtpViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Fact]
        public void AddEditTextToPopupTitle_Always_False()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var model = new RemoveOtpViewModel(request);

            Assert.False(model.AddEditTextToPopupTitle);
        }

        [Theory]
        [InlineData(4, 3, true, true)]
        [InlineData(4, 3, false, false)]
        [InlineData(4, 2, true, false)]
        [InlineData(4, 2, false, false)]
        [InlineData(4, 1, true, false)]
        [InlineData(4, 1, false, false)]
        [InlineData(3, 2, true, true)]
        [InlineData(3, 2, false, false)]
        [InlineData(3, 1, false, false)]
        [InlineData(3, 1, true, false)]
        [InlineData(2, 1, true, false)]
        [InlineData(2, 1, false, false)]
        public void Visible_Return_CorrectValue(
           byte actorLevel,
           byte searchAccountLevel,
           bool hasUpdateAccountPermission,
           bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);

            var viewModel = new RemoveOtpViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Visible);
        }
    }
}
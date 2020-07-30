namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmAllowOutrightViewModel")]
    public class ConfirmAllowOutrightViewModelTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_isAllowedOutright(bool isAllowedOutright, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedOutright", isAllowedOutright);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }

        [Fact]
        public void Disabled_Is_True_When_Upline_Does_Not_AllowedOutright()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedOutright", false);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Fact]
        public void Disabled_Is_False_When_Actor_HasUpdateAccountPermission_And_Upline_Is_AllowedOutright()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedOutright", true);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.False(viewModel.Disabled);
        }

        [Theory]
        [InlineData(true, "InCaseDisabledMessage")]
        [InlineData(false, "InCaseEnabledMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool isAllowedOutright, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedOutright", true);
            request.AddData("SearchAccount.IsAllowedOutright", isAllowedOutright);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_CorrectStatusValue_By_Checked(bool isAllowedOutright, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedOutright", true);
            request.AddData("SearchAccount.IsAllowedOutright", isAllowedOutright);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }

        [Fact]
        public void Always_Return_Correct_SyncedCasino3rd_StatusName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedOutright", true);

            var viewModel = new ConfirmAllowOutrightViewModel(request);

            Assert.Equal("DisableOutright", viewModel.StatusName);
        }
    }
}
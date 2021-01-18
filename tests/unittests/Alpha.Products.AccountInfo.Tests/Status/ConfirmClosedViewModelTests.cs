namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmClosedViewModel")]
    public class ConfirmClosedViewModelTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsClosed(bool isClosed, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsClosed", isClosed);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }

        [Fact]
        public void Disabled_Is_True_When_Upline_Is_Closed()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsClosed", true);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Fact]
        public void Disabled_Is_False_When_Actor_HasUpdateAccountPermission_And_Upline_Doesnt_Close()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsClosed", false);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.False(viewModel.Disabled);
        }

        [Theory]
        [InlineData(false, 2, "InCaseClosedDownlinesMessage")]
        [InlineData(false, 3, "InCaseClosedDownlinesMessage")]
        [InlineData(true, 2, "InCaseChangeClosedStatusMessage")]
        [InlineData(true, 3, "InCaseChangeClosedStatusMessage")]
        [InlineData(true, 1, "InCaseChangeClosedStatusMessage")]
        [InlineData(false, 1, "InCaseChangeClosedStatusMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool closed, byte searchAccountRoleId, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, searchAccountRoleId);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.IsClosed", closed);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_CorrectStatusValue_By_Checked(bool closed, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.IsClosed", closed);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.Equal("Closed", viewModel.StatusName);
            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }


        [Fact]
        public void Always_Reload_CustomerList()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.True(viewModel.ReloadCustomerInfo);
        }

        [Fact]
        public void Always_ShowOn_CustomerList()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.True(viewModel.ShowInCustomerList);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        public void Disabled_Return_CorrectValueByLevel(
           bool actorhasUpdateAccountPermission,
           bool uplineIsClosed,
           bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", actorhasUpdateAccountPermission);
            request.AddData("SearchAccount.Upline.IsClosed", uplineIsClosed);

            var viewModel = new ConfirmClosedViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Disabled);
        }
    }
}
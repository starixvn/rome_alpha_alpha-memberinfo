namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Status.ViewModels;
    using System;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmAllowAutoPTViewModel")]
    public class ConfirmAllowAutoPTViewModelTests
    {
        private const string BaseAddress = "UpdateCustomerStatus";

        [Fact]
        public void Url_Start_With_BaseAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.StartsWith(BaseAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Visible_Is_True_When_Actor_Is_Super_And_SearchAccount_Is_Master()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.True(viewModel.Visible);
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(4, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 1)]
        [InlineData(2, 1)]
        public void Visible_Is_False_When_Actor_Is_Not_Super_Or_SearchAccount_Is_Not_Master(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.False(viewModel.Visible);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsAllowedMasterAutoPT(bool isAllowedMasterAutoPT, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }

        [Theory]
        [InlineData(true, "InCaseDisabledMessage")]
        [InlineData(false, "InCaseEnabledMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool isAllowedMasterAutoPT, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_CorrectStatusValue_By_Checked(bool isAllowedMasterAutoPT, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }

        [Fact]
        public void Always_Return_Correct_SyncedCasino3rd_StatusName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", true);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal("DisableAutoMPT", viewModel.StatusName);
        }

        [Theory]
        [InlineData(false, "ConfirmAllowAutoMasterPTViewModelInCaseEnabledMessage")]
        [InlineData(true, "ConfirmAllowAutoMasterPTViewModelInCaseDisabledMessage")]
        public void ConfirmMessage_Return_CorrectMessage(bool isAllowedMasterAutoPT, string expectedMessage)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedMessage, viewModel.ConfirmMessage);
        }

        [Fact]
        public void Always_Show_On_CustomerList()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.False(viewModel.ReloadCustomerInfo);
        }

        [Fact]
        public void Always_Not_OpenPopup()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.False(viewModel.IsPopup);
        }

        [Fact]
        public void StatusName_Return_CorrectName()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal("DisableAutoMPT", viewModel.StatusName);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void StatusValue_Return_CorrectValue(bool isAllowedMasterAutoPT, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", isAllowedMasterAutoPT);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }


        [Fact]
        public void Params_Return_CorrectValue()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedMasterAutoPT", true);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal("2", viewModel.Params["SearchAccountId"]);
            Assert.Equal("username1", viewModel.Params["SearchAccountName"]);
            Assert.Equal("3", viewModel.Params["SearchAccountRoleId"]);
            Assert.Equal("DisableAutoMPT", viewModel.Params["StatusName"]);
            Assert.True((bool)viewModel.Params["StatusValue"]);
        }

        [Fact]
        public void ShowInCustomerList_Always_True()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.True(viewModel.ShowInCustomerList);
        }

        [Theory]
        [InlineData(4, 3, true)]
        [InlineData(4, 2, false)]
        [InlineData(4, 1, false)]
        [InlineData(3, 2, false)]
        [InlineData(3, 1, false)]
        [InlineData(2, 1, false)]
        public void Visible_Return_CorrectValue(
            byte actorLevel,
            byte searchAccountLevel,
            bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);

            var viewModel = new ConfirmAllowAutoMasterPTViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Visible);
        }
    }
}
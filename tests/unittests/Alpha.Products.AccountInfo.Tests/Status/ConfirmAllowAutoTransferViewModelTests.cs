namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using System;
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmAllowAutoTransferViewModel")]
    public class ConfirmAllowAutoTransferViewModelTests
    {
        private const string ExpectedAutoTransferUpdateAddress = "/autotransfer/update";

        [Fact]
        public void Url_Ends_With_AutoTransferUpdateAddress()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.EndsWith(ExpectedAutoTransferUpdateAddress, viewModel.Url, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsAllowAutoTransfer(bool isAllowAutoTransfer, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }

        [Theory]
        [InlineData(true, "InCaseDisabledMemberMessage")]
        [InlineData(false, "InCaseEnabledMemberMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool isAllowAutoTransfer, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_CorrectStatusValue_By_Checked(bool isAllowAutoTransfer, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }


        [Theory]
        [InlineData(false, "ConfirmAllowAutoTransferViewModelInCaseEnabledMemberMessage")]
        [InlineData(true, "ConfirmAllowAutoTransferViewModelInCaseDisabledMemberMessage")]
        public void ConfirmMessage_Return_CorrectMessage(bool isAllowAutoTransfer, string expectedMessage)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedMessage, viewModel.ConfirmMessage);
        }

        [Fact]
        public void Always_Show_On_CustomerList()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.False(viewModel.ReloadCustomerInfo);
        }

        [Fact]
        public void Always_Not_OpenPopup()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.False(viewModel.IsPopup);
        }

        [Fact]
        public void StatusName_Return_CorrectName()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal("AutoTransfer", viewModel.StatusName);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Checked_Return_CorrectValue(bool isAllowAutoTransfer, bool expectedCheckedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", isAllowAutoTransfer);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedCheckedValue, viewModel.Checked);
        }


        [Fact]
        public void Params_Return_CorrectValue()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowAutoTransfer", true);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal("2", viewModel.Params["CustId"]);
            Assert.Equal("off", viewModel.Params["State"]);
            Assert.Equal(1, viewModel.Params["UseDifferentSite"]);
        }

        [Fact]
        public void ShowInCustomerList_Always_True()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.True(viewModel.ShowInCustomerList);
        }

        [Theory]
        [InlineData(4, 3, false, true)]
        [InlineData(4, 3, true, false)]
        [InlineData(4, 2, true, true)]
        [InlineData(4, 1, true, true)]
        [InlineData(4, 3, true, false)]
        public void Disable_Return_CorrectValue(
            byte actorLevel,
            byte searchAccountLevel,
            bool hasUpdateAccountPermission,
            bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);

            var viewModel = new ConfirmAllowAutoTransferViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Disabled);
        }
    }
}
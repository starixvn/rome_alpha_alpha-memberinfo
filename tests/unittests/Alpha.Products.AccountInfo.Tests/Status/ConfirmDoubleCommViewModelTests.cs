namespace Alpha.Products.AccountInfo.Tests.ViewModels
{
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Utility;
    using Xunit;

    [Trait("Status.ViewModels", "ConfirmDoubleCommViewModel")]
    public class ConfirmDoubleCommViewModelTests
    {
        [Fact]
        public void Disabled_Is_True_When_SearchAccount_Upline_Does_Not_Allow_DoubleComm()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", false);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [ClassData(typeof(ActorAndCrossDownlineSearchAccountLevelTestData))]
        public void Disabled_Is_True_When_SearchAccount_Is_Not_DirectDownline_Of_Actor(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.Disabled);
        }

        [Theory]
        [ClassData(typeof(ActorAndDirectDownlineSearchAccountLevelTestData))]
        public void Disabled_Is_False_When_Actor_HasUpdateAccountPermission_And_Edit_DirectDownline_And_SearchAccount_Upline_Allow_DoubleComm(byte actorLevel, byte searchAccountLevel)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.False(viewModel.Disabled);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Return_Checked_By_SearchAccount_IsAllowedDoubleComm(bool isAllowedDoubleComm, bool expectedValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedDoubleComm", isAllowedDoubleComm);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.Equal(expectedValue, viewModel.Checked);
        }

        [Theory]
        [InlineData(true, 1, "InCaseDisabledMemberMessage")]
        [InlineData(false, 1, "InCaseEnabledMemberMessage")]
        [InlineData(true, 2, "InCaseDisabledMasterOrAgentMessage")]
        [InlineData(false, 2, "InCaseEnableMasterOrAgentMessage")]
        [InlineData(true, 3, "InCaseDisabledMasterOrAgentMessage")]
        [InlineData(false, 3, "InCaseEnableMasterOrAgentMessage")]
        public void Return_CorrectSuffixMessage_By_Checked(bool isAllowedDoubleComm, byte searchAccountRoleId, string expectedSuffixMessages)
        {
            var request = RequestHelper.CreateRequest(4, searchAccountRoleId);
            request.AddData("SearchAccount.IsAllowedDoubleComm", isAllowedDoubleComm);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);


            Assert.Equal(expectedSuffixMessages, viewModel.SuffixMessage);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Return_CorrectStatusValue_By_Checked(bool isAllowedDoubleComm, bool expectedStatusValue)
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedDoubleComm", isAllowedDoubleComm);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.Equal(expectedStatusValue, viewModel.StatusValue);
        }

        [Fact]
        public void Always_Return_Correct_SyncedCasino3rd_StatusName()
        {
            var request = RequestHelper.CreateRequest(4, 3);
            request.AddData("SearchAccount.IsAllowedDoubleComm", true);
            request.AddData("Actor.HasUpdateAccountPermission", true);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", true);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.Equal("DoubleCommLosing", viewModel.StatusName);
        }

        [Fact]
        public void Always_ShowOn_CustomerList()
        {
            var request = RequestHelper.CreateRequest(4, 3);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.True(viewModel.ShowInCustomerList);
        }

        [Theory]
        [InlineData(4, 3, false, true, true)]
        [InlineData(4, 3, true, false, true)]
        [InlineData(4, 2, true, true, true)]
        [InlineData(4, 1, true, true, true)]
        [InlineData(4, 3, true, true, false)]
        public void Disable_Return_CorrectValue(
            byte actorLevel,
            byte searchAccountLevel,
             bool hasUpdateAccountPermission,
             bool uplineIsAllowedDoubleComm,
             bool expectedStatus)
        {
            var request = RequestHelper.CreateRequest(actorLevel, searchAccountLevel);
            request.AddData("Actor.HasUpdateAccountPermission", hasUpdateAccountPermission);
            request.AddData("SearchAccount.Upline.IsAllowedDoubleComm", uplineIsAllowedDoubleComm);

            var viewModel = new ConfirmDoubleCommViewModel(request);

            Assert.Equal(expectedStatus, viewModel.Disabled);
        }
    }
}
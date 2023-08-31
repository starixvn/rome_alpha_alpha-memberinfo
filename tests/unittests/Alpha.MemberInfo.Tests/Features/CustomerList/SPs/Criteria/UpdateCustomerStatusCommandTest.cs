namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class UpdateCustomerStatusCriteriaTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_Should_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var command = new UpdateCustomerStatusCommand(searchAccountId, 1462, 0, "E", "192.168.1.1", StatusNames.Closed);

            Assert.Equal(expectedValue, command.IsValid());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_ActorId_Should_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            var command = new UpdateCustomerStatusCommand(123, actorId, 0, "E", "192.168.1.1", StatusNames.Closed);

            Assert.Equal(expectedValue, command.IsValid());
        }

        [Theory]
        [InlineData(StatusNames.DisableAutoMasterPT, "Acc_CustomerUpdMaster_DisableAutoMPT")]
        [InlineData(StatusNames.Closed, "Acc_CustomerUpdClosed")]
        [InlineData(StatusNames.DoubleComm, "Acc_CustomerUpdDoubleCommLosing")]
        [InlineData(StatusNames.DisableOutright, "Acc_CustomerUpdDisableOutright")]
        [InlineData(StatusNames.Suspended, "Acc_CustomerUpdSuspended")]
        [InlineData(StatusNames.Unlock, "Acc_CustomerUpdLocked")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(string statusName, string expectedSettingKey)
        {
            var command = new UpdateCustomerStatusCommand(1, 2, 0, "E", "192.168.1.1", statusName);
            var actualSettingKey = command.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
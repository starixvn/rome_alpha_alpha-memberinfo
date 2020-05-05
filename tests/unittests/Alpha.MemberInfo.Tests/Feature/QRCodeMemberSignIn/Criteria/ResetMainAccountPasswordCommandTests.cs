namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class ResetMainAccountPasswordCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "ResetMainAccountPassword";
            var criteria = new ResetMainAccountPasswordCommand
            {
                CustId = 1,
                UserPwd = "aaa",
                UserId = 2,
                DisCasino = true,
                Language = "En",
                SubAccId = 0,
                LoginIP = "192.168.1.1"
            };

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputActorId_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new ResetMainAccountPasswordCommand
            {
                CustId = custId,
                UserPwd = "aaa",
                UserId = 2,
                DisCasino = true,
                Language = "En",
                SubAccId = 0,
                LoginIP = "192.168.1.1"
            };

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

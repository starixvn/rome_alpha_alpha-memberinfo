namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class InsertMemberCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustomerInsMember_Ag";
            var criteria = new InsertMemberCommand
            {
                CustId = 1,
                CopyFromCustId = 2,
                Username = "User Test",
                ActorId = 1000
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
        public void IsValid_InputCopyFromCustId_ReturnsCorrectValue(int copyFromCustId, bool expectedValue)
        {
            // Arrange
            var criteria = new InsertMemberCommand
            {
                CustId = 1,
                CopyFromCustId = copyFromCustId,
                Username = "User Test",
                ActorId = 1000
            };

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputActorId_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            // Arrange
            var criteria = new InsertMemberCommand
            {
                CustId = 1,
                CopyFromCustId = 2,
                Username = "User Test",
                ActorId = actorId
            };

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("UserTest", true)]
        public void IsValid_InputUserName_ReturnsCorrectValue(string username, bool expectedValue)
        {
            // Arrange
            var criteria = new InsertMemberCommand
            {
                CustId = 1,
                CopyFromCustId = 2,
                Username = username,
                ActorId = 1000
            };

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

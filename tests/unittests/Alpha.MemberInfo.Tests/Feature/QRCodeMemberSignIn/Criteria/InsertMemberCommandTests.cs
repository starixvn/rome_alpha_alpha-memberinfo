namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Tests.Fixtures;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("AnyValueCreatorCollection")]
    public class InsertMemberCommandTests
    {
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly SportBookSettingModel setting;

        public InsertMemberCommandTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            setting = anyValueCreatorFixture.Create<SportBookSettingModel>();
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustomerInsMember_Ag";

            var criteria = new InsertMemberCommand(
                setting,
                "username",
                "firstname",
                1,
                2,
                "192.168.1.1",
                "En",
                0,
                10);

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
            var criteria = new InsertMemberCommand(
                setting,
                "username",
                "firstname",
                1,
                copyFromCustId,
                "192.168.1.1",
                "En",
                0,
                10);

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
            var criteria = new InsertMemberCommand(
                 setting,
                 "username",
                 "firstname",
                 actorId,
                 2,
                 "192.168.1.1",
                 "En",
                 0,
                 10);

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
            var criteria = new InsertMemberCommand(
                  setting,
                 username,
                  "firstname",
                  1,
                  2,
                  "192.168.1.1",
                  "En",
                  0,
                  10);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

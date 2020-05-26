namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class SelectRnGStatusCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustomerSelRnGStatus";
            var criteria = new SelectRnGStatusCriteria("User Test");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("aa", true)]
        public void IsValid_InputUserName_ReturnsCorrectValue(string username, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectRnGStatusCriteria(username);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

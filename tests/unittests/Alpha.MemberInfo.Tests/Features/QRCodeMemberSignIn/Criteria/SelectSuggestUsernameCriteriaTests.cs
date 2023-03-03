namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class SelectSuggestUsernameCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelSuggestUsername";
            var criteria = new SelectSuggestUsernameCriteria("User Test", 1, 2);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void RoleId_Is_Agent()
        {
            // Arrange
            var criteria = new SelectSuggestUsernameCriteria("User Test", 1, 2);

            // Assert
            Assert.Equal(2, criteria.RoleId);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("aa", true)]
        public void IsValid_InputActorName_ReturnsCorrectValue(string actorName, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectSuggestUsernameCriteria(actorName, 1, 2);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }


        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputCustId_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectSuggestUsernameCriteria("Test", custId, 2);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class SelectAutoSuggestionCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelUserSearch";
            var criteria = new SelectAutoSuggestionCriteria(1, "aa", false, false, false, false);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputActorId_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectAutoSuggestionCriteria(actorId, "aa", false, false, false, false);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

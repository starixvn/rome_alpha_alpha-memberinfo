namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectAutoSuggestionCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelUserSearch";
            var criteria = new SelectAutoSuggestionCriteria(8, "Test", true, true, false);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_WhenAllActorId_ReturnsCorrectValue(int actorId, bool expectedIsValid)
        {
            // Arrange
            var criteria = new SelectAutoSuggestionCriteria(actorId, "Test", true, true, false);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedIsValid, actualValue);
        }

        [Theory]
        [InlineData("user", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_WhenAllSuggestionQuery_ReturnsCorrectValue(string suggestionQuery, bool expectedIsValid)
        {
            // Arrange
            var criteria = new SelectAutoSuggestionCriteria(1, suggestionQuery, true, true, false);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.Equal(expectedIsValid, actualValue);
        }
    }
}
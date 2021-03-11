namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectAutoSuggestionCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Age_CustomerSelUserSearch";
            var criteria = new SelectAutoSuggestionCriteria(8, "Test", true, true, false);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(int actorId, bool expectedIsValid)
        {
            var criteria = new SelectAutoSuggestionCriteria(actorId, "Test", true, true, false);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Theory]
        [InlineData("user", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValid_SuggestionQuery_Should_ReturnsValidSettingKey(string suggestionQuery, bool expectedIsValid)
        {
            var criteria = new SelectAutoSuggestionCriteria(1, suggestionQuery, true, true, false);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
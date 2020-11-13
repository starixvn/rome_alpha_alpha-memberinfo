namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectAgentListCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Age_CustomerSelByMAg";
            var criteria = new SelectAgentListCriteria(8, 1, "Test", true, true, true, 2, 2, 100, 1);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(int mrecommend, bool expectedIsValid)
        {
            var criteria = new SelectAgentListCriteria(8, mrecommend, "Test", true, true, true, 2, 2, 100, 1);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
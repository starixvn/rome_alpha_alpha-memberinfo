namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectMasterListCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Age_CustomerSelBySAg";
            var criteria = new SelectMasterListCriteria(1, "Test", true, true, true, 2, 100, 1);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(int srecommend, bool expectedIsValid)
        {
            var criteria = new SelectMasterListCriteria(srecommend, "Test", true, true, true, 2, 100, 1);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
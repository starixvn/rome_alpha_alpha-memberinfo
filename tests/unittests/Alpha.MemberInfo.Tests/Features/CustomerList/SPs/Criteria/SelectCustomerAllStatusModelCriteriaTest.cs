namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectCustomerAllStatusModelCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Age_CustomerSelAllStatus";
            var criteria = new SelectCustomerAllStatusModelCriteria(8);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(int searchAccountId, bool expectedIsValid)
        {
            var criteria = new SelectCustomerAllStatusModelCriteria(searchAccountId);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
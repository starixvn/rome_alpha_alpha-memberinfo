namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectUnsyncProductsCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Acc_CustomerSelUnSyncProducts";
            var criteria = new SelectUnsyncProductsCriteria(1);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(int custId, bool expectedIsValid)
        {
            var criteria = new SelectUnsyncProductsCriteria(custId);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
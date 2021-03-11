namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class SelectSyncedCasinoListTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Age_CustomerSelSyncCasino";
            var criteria = new SelectSyncedCasinoList("1,2");

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1,2", true)]
        public void IsValid_MRecommend_Should_ReturnsValidSettingKey(string custIds, bool expectedIsValid)
        {
            var criteria = new SelectSyncedCasinoList(custIds);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
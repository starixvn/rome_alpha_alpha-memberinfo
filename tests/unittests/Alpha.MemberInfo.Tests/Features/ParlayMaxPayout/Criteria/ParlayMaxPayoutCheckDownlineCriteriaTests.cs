namespace Alpha.MemberInfo.Tests.Features.ParlayMaxPayout.Criteria
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutCheckDownlineCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustSetting_MaxPayoutCheckDownline";
            var criteria = new ParlayMaxPayoutCheckDownlineCriteria(14373487, string.Empty, "vi");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, "<ROOT></ROOT>", true)]
        [InlineData(1, "", false)]
        [InlineData(0, "<ROOT></ROOT>", false)]
        [InlineData(0, "", false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, string maxPayoutLimit, bool expectedValue)
        {
            // Arrange
            var criteria = new ParlayMaxPayoutCheckDownlineCriteria(custId, maxPayoutLimit, "vi");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}
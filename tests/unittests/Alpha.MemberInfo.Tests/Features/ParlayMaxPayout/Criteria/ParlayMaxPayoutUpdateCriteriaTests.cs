namespace Alpha.MemberInfo.Tests.Features.ParlayMaxPayout.Criteria
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutUpdateCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustSetting_MaxPayoutUpd";
            var criteria = new ParlayMaxPayoutUpdateCriteria(14373487, string.Empty, 14373488, 14373489, "vi", "10.19.10.81");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, "<ROOT></ROOT>", 1, true)]
        [InlineData(1, "<ROOT></ROOT>", 0, false)]
        [InlineData(1, "", 1, false)]
        [InlineData(1, "", 0, false)]
        [InlineData(0, "<ROOT></ROOT>", 1, false)]
        [InlineData(0, "<ROOT></ROOT>", 0, false)]
        [InlineData(0, "", 1, false)]
        [InlineData(0, "", 0, false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, string maxPayoutLimit, int userId, bool expectedValue)
        {
            // Arrange
            var criteria = new ParlayMaxPayoutUpdateCriteria(custId, maxPayoutLimit, userId, 14373489, "vi", "10.19.10.81");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}
namespace Alpha.MemberInfo.Tests.Features.ParlayMaxPayout.Criteria
{
    using Alpha.MemberInfo.Features.ParlayMaxPayout.Criteria;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class ParlayMaxPayoutSelectCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustSetting_MaxPayoutSelect";
            var criteria = new ParlayMaxPayoutSelectCriteria(14373487);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, bool expectedValue)
        {
            // Arrange
            var criteria = new ParlayMaxPayoutSelectCriteria(custId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}
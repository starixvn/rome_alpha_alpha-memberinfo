namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Criteria;
    using Xunit;

    [Trait("Category", "ParlayMaxPayout")]
    public class CheckUMCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CheckUM";
            var criteria = new CheckUMCriteria(14373487, 1, "vi");

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
            var criteria = new CheckUMCriteria(custId, 1, "vi");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}
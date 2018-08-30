namespace Alpha.MemberInfo.Tests.Criteria
{
    using MemberInfo.Criteria;
    using Xunit;

    [Trait("Category", "MemberInformation")]
    public class GetAgentListCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelAgentList";
            var criteria = new GetAgentListCriteria(1);

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
            var criteria = new GetAgentListCriteria(custId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

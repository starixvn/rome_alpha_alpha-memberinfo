namespace Alpha.MemberInfo.Tests.Criteria.Transfer
{
    using Alpha.MemberInfo.Criteria.Transfer;
    using Xunit;

    [Trait("Category", "Transfer")]
    public class GetIsAllDayBalTransferCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetIsAllDayBalTransferCriteria(1176086);
            var expectedSettingKey = "Age_GetIsAllDayBalTransfer";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1176086, true)]
        [InlineData(0, false)]
        [InlineData(-1176086, false)]
        public void IsValid_Always_ReturnsCorrectValue(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new GetIsAllDayBalTransferCriteria(custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}

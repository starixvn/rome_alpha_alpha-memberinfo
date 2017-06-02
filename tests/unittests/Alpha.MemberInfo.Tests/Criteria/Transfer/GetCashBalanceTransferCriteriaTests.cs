using Alpha.MemberInfo.Criteria.Transfer;
using Xunit;

namespace Alpha.MemberInfo.Tests.Criteria.Transfer
{
    [Trait("Category", "Transfer")]
    public class GetCashBalanceTransferCriteriaTests
    {
        [Fact]
        public void GetCashBalanceTransferCriteria_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_Transfer_CashBalance_Sel";
            var criteria = new GetCashBalanceTransferCriteria(1176083);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(-1176083, false)]
        [InlineData(0, false)]
        [InlineData(1176083, true)]
        public void IsValid_Always_ReturnsTrue(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new GetCashBalanceTransferCriteria(custId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualIsValid);
        }
    }
}

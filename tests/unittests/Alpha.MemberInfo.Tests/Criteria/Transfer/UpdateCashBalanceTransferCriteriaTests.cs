using Alpha.MemberInfo.Criteria.Transfer;
using Xunit;

namespace Alpha.MemberInfo.Tests.Criteria.Transfer
{
    [Trait("Category", "Transfer")]
    public class UpdateCashBalanceTransferCriteriaTests
    {
        [Fact]
        public void UpdateCashBalanceTransferCriteria_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_Transfer_Ins_Member";
            var criteria = new UpdateCashBalanceTransferCriteria(1176070, 1176083, 1101240.32M, string.Empty, 0, "127.0.0.1", "E");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1176070, 1176083, 1101240.32, "127.0.0.1", true)]
        [InlineData(-1176070, 1176083, 1101240.32, "127.0.0.1", false)]
        [InlineData(1176070, 1176083, 0, "127.0.0.1", false)]
        [InlineData(1176070, 1176083, 0, null, false)]
        public void IsValid_Always_ReturnsTrue(int uplineId, int custId, decimal amount, string ip, bool expectedResult)
        {
            // Arrange
            var criteria = new UpdateCashBalanceTransferCriteria(uplineId, custId, amount, string.Empty, 0, ip, string.Empty);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualIsValid);
        }
    }
}

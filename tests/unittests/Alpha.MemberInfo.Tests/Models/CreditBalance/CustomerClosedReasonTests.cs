namespace Alpha.MemberInfo.Tests.Models.CreditBalance
{
    using MemberInfo.Models.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CustomerClosedReasonTests
    {
        [Fact]
        public void DefaultConstructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new CustomerClosedReason();

            // Act & Assert
            Assert.True(
                model.CustId == 0
                && model.ClosedReason == 0);
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new CustomerClosedReason(1176070, 0, 2);

            // Act & Assert
            Assert.True(model.CustId == 1176070 && model.Locked == 3);
        }
    }
}

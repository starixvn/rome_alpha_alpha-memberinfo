namespace Alpha.MemberInfo.Tests.Models.CreditBalance
{
    using MemberInfo.Models.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class AgentNetPositionTests
    {
        [Fact]
        public void DefaultConstructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new AgentNetPosition();

            // Act & Assert
            Assert.True(
                model.CustId == 0
                && model.NetPosition == 0);
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new AgentNetPosition(1176070, -5139959.11m);

            // Act & Assert
            Assert.True(
                model.CustId == 1176070
                && model.NetPosition == -5139959.11m);
        }
    }
}

namespace Alpha.MemberInfo.Tests.Models
{
    using MemberInfo.Models;
    using Xunit;

    [Trait("Category", "MemberInfo")]
    public class CustomerTransferStatusTests
    {
        [Fact]
        public void DefaultConstructor_Always_NotSetData()
        {
            // Act
            var model = new CustomerTransferStatus();

            // Assert
            Assert.True(model.CustId == 0 && !model.IsAllDayBalTransfer);
        }

        [Fact]
        public void ConstructorWinIdAndName_Always_SetData()
        {
            // Act
            var model = new CustomerTransferStatus(1, true);

            // Assert
            Assert.True(model.CustId == 1 && model.IsAllDayBalTransfer);
        }
    }
}

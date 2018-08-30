namespace Alpha.MemberInfo.Tests.Models
{
    using MemberInfo.Models;
    using Xunit;

    [Trait("Category", "MemberInfo")]
    public class CustomerInfoTests
    {
        [Fact]
        public void DefaultConstructor_Always_NotSetData()
        {
            // Act
            var model = new CustomerInfo();

            // Assert
            Assert.True(model.CustId == 0 && model.Username == null);
        }

        [Fact]
        public void ConstructorWinIdAndName_Always_SetData()
        {
            // Act
            var model = new CustomerInfo("CustName", 1);

            // Assert
            Assert.True(model.CustId == 1 && model.Username == "CustName");
        }
    }
}

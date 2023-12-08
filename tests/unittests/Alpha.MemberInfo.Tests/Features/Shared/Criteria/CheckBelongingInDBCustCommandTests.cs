namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Alpha.MemberInfo.Features.Shared.Params;
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class CheckBelongingInDBCustCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelCheckBelonging_DBCUST";
            var param = new CheckBelongingParam(1, 1, 1);
            var criteria = new CheckBelongingInDBCustCommand(
                param.ViewerId, param.CustId, param.SubAccId, param.TransId);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 1, 1)]
        public void IsValid_ValidIds_ReturnsTrue(int viewerId, int custId, int subAccId, int transId)
        {
            // Arrange
            var param = new CheckBelongingParam(viewerId, custId, subAccId, transId);
            var criteria = new CheckBelongingInDBCustCommand(
                param.ViewerId, param.CustId, param.SubAccId, param.TransId);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }

        [Theory]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 1, -1, 1)]
        [InlineData(1, 0, 1, 0)]
        public void IsValid_InvalidIds_ReturnsFalse(int viewerId, int custId, int subAccId, int transId)
        {
            // Arrange
            var param = new CheckBelongingParam(viewerId, custId, subAccId, transId);
            var criteria = new CheckBelongingInDBCustCommand(
                param.ViewerId, param.CustId, param.SubAccId, param.TransId);

            // Act
            var actualValue = criteria.IsValid();

            // Assert
            Assert.False(actualValue);
        }
    }
}
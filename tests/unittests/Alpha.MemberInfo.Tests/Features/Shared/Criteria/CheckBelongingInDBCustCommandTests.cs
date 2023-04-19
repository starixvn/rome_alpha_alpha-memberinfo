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
            var expectedSettingKey = "Age_CustomerSelCheckBelonging_DBCUST";
            var param = new CheckBelongingParam(1, 1, 1);
            var criteria = new CheckBelongingInDBCustCommand(param);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(actualSettingKey, expectedSettingKey);
        }

        [Theory]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 1, 1)]
        public void IsValid_Always_ReturnsTrue(int viewerId, int custId, int subAccId, int transId)
        {
            var param = new CheckBelongingParam(viewerId, custId, subAccId, transId);
            var criteria = new CheckBelongingInDBCustCommand(param);

            Assert.True(criteria.IsValid());
        }

        [Theory]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 1, -1, 1)]
        [InlineData(1, 0, 1, 0)]

        public void IsValid_Always_ReturnsFalse(int viewerId, int custId, int subAccId, int transId)
        {
            var param = new CheckBelongingParam(viewerId, custId, subAccId, transId);
            var criteria = new CheckBelongingInDBCustCommand(param);

            Assert.False(criteria.IsValid());
        }
    }
}
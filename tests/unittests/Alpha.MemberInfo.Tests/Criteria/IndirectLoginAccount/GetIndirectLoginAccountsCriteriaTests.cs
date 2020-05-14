namespace Alpha.MemberInfo.Tests.Criteria.IndirectLoginAccount
{
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class GetIndirectLoginAccountsCriteriaTests
    {
        [Fact]
        public void Constructor_Always_AssignsDefaultValues()
        {
            // Arrange & Act
            var criteria = new GetIndirectLoginAccountsCriteria(1176070, 87413);

            // Assert
            Assert.True(
                criteria.CustId == 1176070
                && criteria.SubAccId == 87413
                && criteria.CustIDInDirect == null
                && criteria.SubAccIDIndirect == null);
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetIndirectLoginAccountsCriteria(1176070, 87413, 1176083, 91726);
            var expected = "Age_CustomerIndirectLoginAccountSel";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1176083, true)]
        [InlineData(0, false)]
        [InlineData(-1176083, false)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
        {
            // Arrange
            var criteria = new GetIndirectLoginAccountsCriteria(custId, 87413, 1176083, 91726);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1176092, true)]
        [InlineData(0, true)]
        [InlineData(-1176092, false)]
        public void IsValid_SubAccId_ReturnsExpectedResult(int subAccId, bool expected)
        {
            // Arrange
            var criteria = new GetIndirectLoginAccountsCriteria(1176083, subAccId);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

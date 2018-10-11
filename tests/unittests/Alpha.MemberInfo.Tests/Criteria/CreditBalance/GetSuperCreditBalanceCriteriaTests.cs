namespace Alpha.MemberInfo.Tests.Criteria.CreditBalance
{
    using MemberInfo.Criteria.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class GetSuperCreditBalanceCriteriaTests
    {
        private const int CustIdTest = 1176070;
        private const string UserNameTest = "master1";
        private readonly bool? EnabledTest = null;
        private readonly bool? ClosedTest = false;
        private readonly bool? SuspendedTest = null;
        private const int PageIndexTest = 1;
        private const int PageSizeTest = 500;

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetSuperCreditBalanceCriteria(
                CustIdTest,
                UserNameTest,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);
            var expected = "Age_CustomerSelCreditBalanceList_Sag";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

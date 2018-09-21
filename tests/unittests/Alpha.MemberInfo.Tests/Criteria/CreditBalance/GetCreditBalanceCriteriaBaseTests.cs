namespace Alpha.MemberInfo.Tests.Criteria.CreditBalance
{
    using System;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class GetCreditBalanceCriteriaBaseTests
    {
        private const int CustIdTest = 1176070;
        private const string UserNameTest = "master1";
        private readonly bool? EnabledTest = null;
        private readonly bool? ClosedTest = false;
        private readonly bool? SuspendedTest = null;
        private const int PageIndexTest = 1;
        private const int PageSizeTest = 500;

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var criteriaBase = new GetCreditBalanceCriteriaBaseForTesting(
                CustIdTest,
                UserNameTest,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);

            // Act & Assert
            Assert.True(
                criteriaBase.UserName.Equals(UserNameTest)
                && criteriaBase.Enabled == EnabledTest
                && criteriaBase.Closed == ClosedTest
                && criteriaBase.Suspended == SuspendedTest);
        }

        [Fact]
        public void Constructor_UsernameIsNull_ReturnsEmpty()
        {
            // Arrange
            var criteriaBase = new GetCreditBalanceCriteriaBaseForTesting(
                CustIdTest,
                null,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);

            // Act & Assert
            Assert.True(criteriaBase.UserName.Length == 0);
        }

        [Fact]
        public void GetSettingKey_Always_ThrowsNotImplementedException()
        {
            // Arrange
            var criteriaBase = new GetCreditBalanceCriteriaBaseForTesting(
                CustIdTest,
                UserNameTest,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => criteriaBase.GetSettingKey());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteriaBase = new GetCreditBalanceCriteriaBaseForTesting(
                CustIdTest,
                UserNameTest,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);

            // Act
            var actualResult = criteriaBase.IsValid();

            // Assert
            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1176070)]
        [InlineData(0)]
        public void IsValid_InvalidCriteria_ReturnsFalse(int custId)
        {
            // Arrange
            var criteriaBase = new GetCreditBalanceCriteriaBaseForTesting(
                custId,
                UserNameTest,
                EnabledTest,
                ClosedTest,
                SuspendedTest,
                PageIndexTest,
                PageSizeTest);

            // Act
            var actualResult = criteriaBase.IsValid();

            // Assert
            Assert.False(actualResult);
        }
    }
}

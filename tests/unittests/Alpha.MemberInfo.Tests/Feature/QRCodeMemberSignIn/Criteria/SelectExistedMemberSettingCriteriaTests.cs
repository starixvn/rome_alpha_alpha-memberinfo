namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class SelectExistedMemberSettingCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Acc_CustomerSelEditCustomer";
            var criteria = new SelectExistedMemberSettingCriteria(1);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputexistedCustId_ReturnsCorrectValue(int existedCustId, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectExistedMemberSettingCriteria(existedCustId);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

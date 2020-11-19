namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class SelectCreditRangeCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerInsMember_Ag_CreditStatusSel";
            var criteria = new SelectCreditAndStatusCriteria("User Test");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("aa", true)]
        public void IsValid_InputSearchAccountName_ReturnsCorrectValue(string searchAccountName, bool expectedValue)
        {
            // Arrange
            var criteria = new SelectCreditAndStatusCriteria(searchAccountName);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

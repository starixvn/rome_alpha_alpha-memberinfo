namespace Alpha.MemberInfo.Tests.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    public class CheckBelongingByUsernameCritieriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelCheckBelonging_ByUsername";
            var criteria = new CheckBelongingByUsernameCritieria(1, "userTest", 0);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InputActorId_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            // Arrange
            var criteria = new CheckBelongingByUsernameCritieria(actorId, "userTest", 0);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.Equal(expectedValue, actualIsValid);
        }
    }
}

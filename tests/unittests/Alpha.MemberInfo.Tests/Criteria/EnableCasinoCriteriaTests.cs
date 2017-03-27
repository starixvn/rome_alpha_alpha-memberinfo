namespace Alpha.MemberInfo.Test.Criteria.MemberRequests
{
    using MemberInfo.Criteria.MemberRequests;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    public class EnableCasinoCriteriaTests
    {
        [Fact]
        public void EnableCasinoCriteria_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_UpdateCasinoStatus";
            var criteria = new EnableCasinoCriteria(15, "Username", 260, 0, "192.168.1.1", "E");

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new EnableCasinoCriteria(15, "Username", 260, 0, "192.168.1.1", "E");

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }
    }
}

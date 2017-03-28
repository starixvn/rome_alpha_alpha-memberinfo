namespace Alpha.MemberInfo.Test.Criteria.MemberRequests
{
    using MemberInfo.Criteria.MemberRequests;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    public class MemberRequestCriteriaTests
    {
        [Fact]
        public void MemberRequestCriteria_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_PlayForFun_MemberRequest_Detail";
            var criteria = new MemberRequestCriteria(15, 6);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new MemberRequestCriteria(15, 6);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }
    }
}

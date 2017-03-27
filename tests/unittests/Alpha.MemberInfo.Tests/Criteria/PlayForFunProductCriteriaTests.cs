namespace Alpha.MemberInfo.Test.Criteria.MemberRequests
{
    using MemberInfo.Criteria.MemberRequests;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    public class PlayForFunProductCriteriaTests
    {
        [Fact]
        public void PlayForFunProductCriteria_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_Rpt_PlayForFun_ProductSel";
            var criteria = new PlayForFunProductCriteria(15);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new PlayForFunProductCriteria(15);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }
    }
}

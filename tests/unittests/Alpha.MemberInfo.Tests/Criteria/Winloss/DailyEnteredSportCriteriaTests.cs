namespace Alpha.MemberInfo.Tests.Criteria.WinLoss
{
    using MemberInfo.Criteria.WinLoss;
    using Xunit;

    [Trait("Category", "WinLoss")]
    public class DailyEnteredSportCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_Rpt_MatchSelResultEntered";
            var criteria = new DailyEnteredSportCriteria(true);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsTrue()
        {
            // Arrange
            var criteria = new DailyEnteredSportCriteria(true);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }
    }
}


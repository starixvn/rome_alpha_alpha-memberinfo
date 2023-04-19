namespace Alpha.MemberInfo.Tests.Models.Winloss
{
    using MemberInfo.Models.Winloss;
    using Xunit;

    [Trait("Category", "WinLoss")]
    public class EnteredSportResultModelTests
    {
        [Fact]
        public void DefaultConstructor_Always_NotSetData()
        {
            // Act
            var model = new EnteredSportResult();

            // Assert
            Assert.True(model.SportTypeId == 0
                && model.SportName == null
                && model.EnteredSportToday == null
                && model.EnteredSportToday == null
                );
        }

        [Fact]
        public void Constructor_Always_SetData()
        {
            // Act
            var dailyEnteredSport = new DailyEnteredSport();
            var enteredSportYesterday = new DailyEnteredSport();
            var model = new EnteredSportResult(1, "SportName", dailyEnteredSport, enteredSportYesterday);

            // Assert
            Assert.True(model.SportTypeId == 1
                && model.SportName == "SportName"
                && model.EnteredSportToday == dailyEnteredSport
                && model.EnteredSportYesterday == enteredSportYesterday);
        }
    }
}

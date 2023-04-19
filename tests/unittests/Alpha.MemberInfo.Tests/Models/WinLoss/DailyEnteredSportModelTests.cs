namespace Alpha.MemberInfo.Tests.Models.Winloss
{
    using Alpha.MemberInfo.Models;
    using MemberInfo.Models.Winloss;
    using System;
    using Xunit;

    [Trait("Category", "WinLoss")]
    public class DailyEnteredSportModelTests
    {
        [Fact]
        public void DefaultConstructor_Always_NotSetData()
        {
            // Act
            var model = new DailyEnteredSport();

            // Assert
            Assert.True(model.Entered == 0 && model.NonEntered == 0);
        }

        [Fact]
        public void ConstructorWithEnteredAndNonEntered_Always_SetData()
        {
            // Act
            var model = new DailyEnteredSport(1, 1);

            // Assert
            Assert.True(model.Entered == 1 && model.NonEntered == 1);
        }


        [Fact]
        public void Constructor_Always_SetData()
        {
            // Act
            var eventDate = DateTime.Now;
            var model = new DailyEnteredSport(true, eventDate, 1, 1, 1);

            // Assert
            Assert.True(
                model.IsToday
                && model.EventDate == eventDate
                && model.SportType == 1
                && model.Entered == 1
                && model.NonEntered == 1
                );
        }
    }
}

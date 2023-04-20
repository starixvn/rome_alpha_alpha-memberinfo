using System;
using System.Collections.Generic;
using System.Linq;
using Alpha.MemberInfo.Models.Winloss;
using Alpha.MemberInfo.Services;
using Fanex.Caching;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace Alpha.MemberInfo.Tests.Services
{
    [Trait("Category", "Winloss")]
    public class WinlossServiceTests
    {
        private readonly WinlossService service;
        private readonly IDynamicRepository repository;
        private readonly ICacheService cacheService;

        public WinlossServiceTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            cacheService = Substitute.For<ICacheService>();
            service = new WinlossService(repository, cacheService);
        }

        [Fact]
        public void GetEnteredSportResults_Always_ReturnCorrectResults()
        {
            // Arrange
            var totalToday = new DailyEnteredSport(11, 6);
            var totalYesterday = new DailyEnteredSport(22, 16);
            var expected = new EnteredSportResult(0, "Total", totalToday, totalYesterday);

            var dailyEnteredSports = new List<DailyEnteredSport>()
            {
                new DailyEnteredSport(true, DateTime.Now ,1,10,5),
                new DailyEnteredSport(true, DateTime.Now ,2,1,1),

                new DailyEnteredSport(false, DateTime.Now ,1,11,6),
                new DailyEnteredSport(false, DateTime.Now ,2,11,10),
            };
            var result = dailyEnteredSports.AsEnumerable();

            cacheService.GetOrSet(Arg.Any<string>(),
                                 Arg.Any<Func<IEnumerable<DailyEnteredSport>>>(),
                                 Arg.Any<CacheItemOptions>()).Returns(result);

            // Act
            var actual = service.GetEnteredSportResults();

            // Assert
            Assert.True(
                expected.EnteredSportToday.Entered == actual.EnteredSportToday.Entered
                && expected.EnteredSportYesterday.Entered == actual.EnteredSportYesterday.Entered);
        }

        [Fact]
        public void GetTotalEnteredSportYesterdayAndToday_Always_ReturnsCorrectResults()
        {
            // Arrange
            var totalToday = new DailyEnteredSport(20, 12);
            var totalYesterday = new DailyEnteredSport(24, 23);
            var expected = new EnteredSportResult(0, "Total", totalToday, totalYesterday);

            var data = new List<EnteredSportResult>() {
                new EnteredSportResult(1,"1", new DailyEnteredSport(10,5), new DailyEnteredSport(12,10)),
                new EnteredSportResult(2,"1", new DailyEnteredSport(10,7), new DailyEnteredSport(12,13)),
            };
            var actual = WinlossService.GetTotalEnteredSportYesterdayAndToday(data);

            // Assert
            Assert.True(expected.EnteredSportToday.Entered == actual.EnteredSportToday.Entered
                && expected.EnteredSportToday.NonEntered == actual.EnteredSportToday.NonEntered
                && expected.EnteredSportYesterday.Entered == actual.EnteredSportYesterday.Entered
                && expected.EnteredSportYesterday.NonEntered == actual.EnteredSportYesterday.NonEntered
                );
        }

        [Fact]
        public void GetDataEnteredSportResults_Always_ReturnCorrectResults()
        {
            // Arrange
            var sportId1 = 1;
            var sportId2 = 2;

            var dailyEnteredSportData = new List<DailyEnteredSport>()
            {
                new DailyEnteredSport(true, DateTime.Now, sportId1,10,5),
                new DailyEnteredSport(true, DateTime.Now, sportId2,10,1),

                new DailyEnteredSport(false, DateTime.Now, sportId1,11,6),
                new DailyEnteredSport(false, DateTime.Now, sportId2,11,10),
            };

            cacheService.GetOrSet(Arg.Any<string>(),
                Arg.Any<Func<IEnumerable<DailyEnteredSport>>>(),
                Arg.Any<CacheItemOptions>()).Returns(dailyEnteredSportData);

            //Act
            var result = service.GetDataEnteredSportResults(false).ToList();

            // Assert
            Assert.True(result.Count == 2);
        }
    }
}

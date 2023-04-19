using System;
using System.Collections.Generic;
using System.Linq;
using Alpha.MemberInfo.Constants;
using Alpha.MemberInfo.Criteria.ProblemAccount;
using Alpha.MemberInfo.Criteria.WinLoss;
using Alpha.MemberInfo.Features.Shared.Criteria;
using Alpha.MemberInfo.Models.ProblemAccount;
using Alpha.MemberInfo.Models.Winloss;
using Alpha.MemberInfo.Params;
using Alpha.MemberInfo.Params.ProblemAccount;
using Alpha.MemberInfo.Services;
using AutoFixture.Kernel;
using Fanex.Caching;
using Fanex.Common.Extensions;
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
            // Act

            cacheService.GetOrSet(Arg.Any<string>(),
                                 Arg.Any<Func<IEnumerable<DailyEnteredSport>>>(),
                                 Arg.Any<CacheItemOptions>()).Returns(result);

            var actual = service.GetEnteredSportResults();

            // Assert
            Assert.Equal(expected.EnteredSportToday.Entered, actual.EnteredSportToday.Entered);
            Assert.Equal(expected.EnteredSportYesterday.Entered, actual.EnteredSportYesterday.Entered);
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
            }.AsEnumerable();

            cacheService.GetOrSet(Arg.Any<string>(),
                                 Arg.Any<Func<IEnumerable<DailyEnteredSport>>>(),
                                 Arg.Any<CacheItemOptions>()).Returns(dailyEnteredSportData);


            var groupData = dailyEnteredSportData.GroupBy(dailyEnteredSport => dailyEnteredSport.SportType)
                        .Select(dailyEnteredSports => new EnteredSportResult(
                        dailyEnteredSports.Key,
                        dailyEnteredSports.Key.ToString(),
                        dailyEnteredSports.FirstOrDefault(enteredSportResult => enteredSportResult.IsToday),
                        dailyEnteredSports.FirstOrDefault(enteredSportResult => !enteredSportResult.IsToday)));


            var expectedTodayEntered = 0;
            var expectedTodayNonEntered = 0;

            var expectedYesterdayEntered = 0;
            var expectedYesterdayNonEntered = 0;


            foreach (var data in groupData)
            {
                if (data.EnteredSportToday != null)
                {
                    expectedTodayEntered += data.EnteredSportToday.Entered;
                    expectedTodayNonEntered += data.EnteredSportToday.NonEntered;
                }

                if (data.EnteredSportYesterday != null)
                {
                    expectedYesterdayEntered += data.EnteredSportYesterday.Entered;
                    expectedYesterdayNonEntered += data.EnteredSportYesterday.NonEntered;
                }
            }
            //Act
            var result = service.GetDataEnteredSportResults(false).ToList();

            var actualTodayEntered = 0;
            var actualTodayNonEntered = 0;

            var actualYesterdayEntered = 0;
            var actualYesterdayNonEntered = 0;

            foreach (var data in result)
            {
                if (data.EnteredSportToday != null)
                {
                    actualTodayEntered += data.EnteredSportToday.Entered;
                    actualTodayNonEntered += data.EnteredSportToday.NonEntered;
                }

                if (data.EnteredSportYesterday != null)
                {
                    actualYesterdayEntered += data.EnteredSportYesterday.Entered;
                    actualYesterdayNonEntered += data.EnteredSportYesterday.NonEntered;
                }
            }

            // Assert
            Assert.True(actualTodayEntered == expectedTodayEntered
                && actualTodayNonEntered == expectedTodayNonEntered
                && actualYesterdayEntered == expectedYesterdayEntered
                && actualYesterdayNonEntered == expectedYesterdayNonEntered
                );
        }

    }
}

namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Data.Repository;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class ReachedThresholdServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReachedThresholdService service;

        public ReachedThresholdServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            service = new ReachedThresholdServiceImpl(dynamicRepository);
        }

        [Fact]
        public async Task GetReachlistAccounts_HasData_ReturnsCorrectResults()
        {
            // Arrange
            var param = new SelectReachListParam(
                1234, 
                "agent0101", 
                DateTime.Today.AddDays(-1), 
                DateTime.Today, 
                "Open", 
                0, 
                0, 
                false, 
                100, 
                1, 
                new DateTime());
            IEnumerable<ReachlistAccountModel> reachlistAccounts = new List<ReachlistAccountModel>()
            {
                new ReachlistAccountModel(1, 1234, 0, "agent0101", new DateTime(), new DateTime(), "Global", "Open", 1000, 1000, 2000, 1, 0, 0, 1, 1),
                new ReachlistAccountModel(2, 1234, 0, "agent0102", new DateTime(), new DateTime(), "Global", "Open", 1000, 1000, 2000, 1, 0, 0, 1, 1)
            };
            var expectedResults = new ReachlistAccountReport(reachlistAccounts, 0);
            dynamicRepository.FetchAsync<ReachlistAccountModel>(Arg.Any<SelectReachListCriteria>())
                .Returns(Task.FromResult(reachlistAccounts));

            // Act
            var actualResults = await service.GetReachlistAccounts(param);

            // Assert
            AssertExtension.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task GetReachDetail_HasData_ReturnCorrectResults()
        {
            // Arrange
            var param = new SelectReachDetailParam(1234, DateTime.Today.AddDays(-1), DateTime.Today, 0, 0, false, new DateTime());
            IEnumerable<ReachHistoryModel> expectedResults = new List<ReachHistoryModel>()
            {
                new ReachHistoryModel(DateTime.Today, "Global", 1000, 1000, 2000, 1),
                new ReachHistoryModel(DateTime.Today, "Global", 2000, 3000, 4000, 2),
            };

            dynamicRepository.FetchAsync<ReachHistoryModel>(Arg.Any<SelectReachDetailCriteria>())
                .Returns(Task.FromResult(expectedResults));

            // Act
            var actualResults = await service.GetReachDetail(param);
            
            // Assert
            AssertExtension.Equal(expectedResults, actualResults);
        }
    }
}

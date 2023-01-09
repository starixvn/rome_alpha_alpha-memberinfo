namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyThresholds
{
    using System;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.PartialUM.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "MyThresholds")]
    public class MyThresholdsServiceTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IPartialUMService partialUMService;
        private readonly MyThresholdsService service;

        public MyThresholdsServiceTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            partialUMService = Substitute.For<IPartialUMService>();
            service = new MyThresholdsServiceImpl(dynamicRepository, partialUMService);
        }

        [Fact]
        public async Task GetMyThresholdHistory_Always_CallFetchAsync()
        {
            // Arrange
            var today = DateTime.Now;
            var param = new SelectMyThresholdHistoryParam(
                1234, today.AddDays(-1), today, 1, true, DateTime.MinValue);

            // Act
            await service.GetMyThresholdHistory(param);

            // Assert
            await dynamicRepository.Received().FetchAsync<ReachHistoryModel>(Arg.Any<SelectMyThresholdHistoryCriteria>());
        }

        [Fact]
        public async Task GetMyThreshold_Always_CallGetAsync()
        {
            // Arrange && Act
            await service.GetMyThreshold(1234);

            // Assert
            await dynamicRepository.Received().GetAsync<GlobalThresholdModel>(Arg.Any<SelectMyThresholdCriteria>());
        }

        [Fact]
        public async Task UpdateMyThreshold_Always_CallExecuteAsync()
        {
            // Arrange
            var param = new UpdateMyThresholdParam(1234, null, 100, 5678, 0, "::1");

            // Act
            await service.UpdateMyThreshold(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Any<UpdateMyThresholdCommand>());
        }
    }
}

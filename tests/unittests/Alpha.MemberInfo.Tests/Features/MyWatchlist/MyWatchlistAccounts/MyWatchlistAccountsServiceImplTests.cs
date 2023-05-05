namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Tests.Extensions;
    using Alpha.PartialUM.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class MyWatchlistAccountsServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IPartialUMService partialUMService;
        private readonly MyWatchlistAccountsService service;

        public MyWatchlistAccountsServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            partialUMService = Substitute.For<IPartialUMService>();
            service = new MyWatchlistAccountsServiceImpl(dynamicRepository, partialUMService);
        }

        [Fact]
        public async Task GetWatchlistAccounts_HasData_ReturnsCorrectResults()
        {
            // Arrange
            var param = new SelectWatchlistAccountsParam(
                1234,
                "agent0101", 
                string.Empty,
                1,
                false,
                1,
                100);
            IEnumerable<WatchlistAccountModel> watchlistAccounts = new List<WatchlistAccountModel>()
            {
                new WatchlistAccountModel(1, 1234, 0, "agent01", DateTime.Today, "Open", 1000, 2000, 0, 0, true),
                new WatchlistAccountModel(2, 1234, 0, "agent01", DateTime.Today, "Open", 1000, 2000, 0, 0, false),
            };
            var expectedResults = new WatchlistAccountReport(watchlistAccounts, 0);
            dynamicRepository.FetchAsync<WatchlistAccountModel>(Arg.Any<SelectWatchlistAccountsCriteria>())
                .Returns(Task.FromResult(watchlistAccounts));

            // Act
            var actualResults = await service.GetWatchlistAccounts(param);

            // Assert
            AssertExtension.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task GetPresets_HasData_ReturnsCorrectResults()
        {
            // Arrange
            var param = new SelectPresetsParam(1234, string.Empty);
            IEnumerable<PersonalPresetModel> expectedResults = new List<PersonalPresetModel>()
            {
                new PersonalPresetModel(),
                new PersonalPresetModel()
            };
            dynamicRepository.FetchAsync<PersonalPresetModel>(Arg.Any<SelectPresetsCriteria>()).Returns(Task.FromResult(expectedResults));

            // Act
            var actualResults = await service.GetPresets(param);

            // Assert
            AssertExtension.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task AddPersonalThreshold_Always_ReturnsCorrectResults()
        {
            // Arrange
            var param = new AddPersonalThresholdParam(
                "agent01",
                1234,
                1000,
                2000,
                1,
                12,
                "192.0.0.1",
                string.Empty);
            IEnumerable<NamedResult> expectedResults = new List<NamedResult>()
            {
                new NamedResult(),
            };
            dynamicRepository.FetchAsync<NamedResult>(Arg.Any<InsertMultiplePersonalThresholdCriteria>()).Returns(Task.FromResult(expectedResults));

            // Act
            var actualResults = await service.AddPersonalThreshold(param);

            // Assert
            AssertExtension.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task UpdatePersonalThreshold_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new UpdatePersonalThresholdParam(
                1234,
                "agent0101",
                1000,
                2000,
                1,
                12,
                "192.0.0.1",
                string.Empty);

            // Act
            await service.UpdatePersonalThreshold(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<UpdatePersonalThresholdCommand>(
                    c => c.CustId == 1234
                    && c.WinningThreshold == 1000
                    && c.LosingThreshold == 2000
                    && c.UserId == 1
                    && c.SubAccId == 12
                    && c.IP == "192.0.0.1"
                    ));
        }

        [Fact]
        public async Task DeletePersonalThreshold_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new DeletePersonalThresholdParam(
                1234,
                "agent0101",
                1,
                12,
                "192.0.0.1",
                string.Empty);

            // Act
            await service.DeletePersonalThreshold(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<DeletePersonalThresholdCommand>(
                    c => c.CustId == 1234
                    && c.UserId == 1
                    && c.SubAccId == 12
                    && c.IP == "192.0.0.1"
                    ));
        }

        [Fact]
        public async Task GetGlobalThreshold_HasData_ReturnsCorrectResults()
        {
            // Arrange
            var param = new SelectGlobalThresholdParam(1234, string.Empty);
            var expectedResult = new GlobalThresholdModel();
            dynamicRepository.GetAsync<GlobalThresholdModel>(Arg.Any<SelectGlobalThresholdCriteria>()).Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = await service.GetGlobalThreshold(param);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UpdateGlobalThreshold_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new UpdateGlobalThresholdParam(
                1234,
                1000,
                2000,
                1,
                12,
                "192.0.0.1",
                string.Empty);

            // Act
            await service.UpdateGlobalThreshold(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<UpdateGlobalThresholdCommand>(
                    c => c.AgentId == 1234
                    && c.WinningThreshold == 1000
                    && c.LosingThreshold == 2000
                    && c.UserId == 1
                    && c.SubAccId == 12
                    && c.IP == "192.0.0.1"
                    ));
        }

        [Fact]
        public async Task AddPersonalPreset_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new AddPersonalPresetParam(
                1234,
                "preset1",
                1000,
                2000,
                string.Empty);

            // Act
            await service.AddPersonalPreset(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<AddPersonalPresetCommand>(
                    c => c.AgentId == 1234
                    && c.PresetTitle == "preset1"
                    && c.WinningThreshold == 1000
                    && c.LosingThreshold == 2000
                    ));
        }

        [Fact]
        public async Task UpdatePersonalPreset_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new UpdatePersonalPresetParam(
                1,
                1234,
                "preset1",
                1000,
                2000
                );

            // Act
            await service.UpdatePersonalPreset(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<UpdatePersonalPresetCommand>(
                    c => c.ID == 1
                    && c.AgentId == 1234
                    && c.PresetTitle == "preset1"
                    && c.WinningThreshold == 1000
                    && c.LosingThreshold == 2000
                    ));
        }

        [Fact]
        public async Task DeletePersonalPreset_Always_CallsExecuteAsynce()
        {
            // Arrange
            var param = new DeletePersonalPresetParam(1, 1234, string.Empty);

            // Act
            await service.DeletePersonalPreset(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<DeletePersonalPresetCommand>(
                    c => c.ID == 1
                    && c.AgentId == 1234
                    ));
        }
    }
}

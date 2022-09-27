namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.MyWatchlistAccounts
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.Requests;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.MyWatchlistAccounts.ViewModels;
    using Alpha.MemberInfo.Site.Tests.Extensions;
    using Alpha.Web.Helpers;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class MyWatchlistAccountsHandlerTests
    {
        private readonly MyWatchlistAccountsService service;
        private readonly MyWatchlistAccountsHandler handler;
        private readonly BrowserHelper browserHelper;
        private readonly IUser user;

        public MyWatchlistAccountsHandlerTests()
        {
            service = Substitute.For<MyWatchlistAccountsService>();
            browserHelper = Substitute.For<BrowserHelper>();

            user = Substitute.For<IUser>();
            user.Id.Returns(1176086);
            user.Name.Returns("agent01");
            user.LevelId.Returns((byte)1);
            user.Language.Returns("en-US");

            handler = new MyWatchlistAccountsHandler(service, browserHelper);
        }

        private WatchlistAccountReport CreateWatchlistAccountReport()
           => new WatchlistAccountReport(
                  new List<WatchlistAccountModel>()
                  {
                        new WatchlistAccountModel(1, 1234, 0, "agent0101", new DateTime(), "Open", null, null, 0, 0),
                        new WatchlistAccountModel(2, 2345, 0, "agent0102", new DateTime(), "Open", null, null, 0, 0),
                  },
              2);

        [Fact]
        public async Task HandleMyWatchlistAccountsQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var myWatchlistAccountsMessage = new MyWatchlistAccountsQuery(user);
            var watchlistAccountsMessage = new WatchlistAccountsQuery(user);

            var watchlistAccountReport = CreateWatchlistAccountReport();
            var globalThreshold = new GlobalThresholdModel(1000, 2000);

            service.GetWatchlistAccounts(Arg.Any<SelectWatchlistAccountsParam>()).Returns(Task.FromResult(watchlistAccountReport));
            service.GetGlobalThreshold(Arg.Any<SelectGlobalThresholdParam>()).Returns(Task.FromResult(globalThreshold));

            var expectedViewModel = new MyWatchlistAccountsViewModel(
                myWatchlistAccountsMessage, 
                globalThreshold,
                browserHelper.IsMobile(isOnlyTablet: false),
                myWatchlistAccountsMessage.CanUpdateAccount, 
                new MyWatchlistAccountsPagingViewModel(watchlistAccountReport, watchlistAccountsMessage));

            // Act
            var actualViewModel = await handler.Handle(myWatchlistAccountsMessage);

            // Assert
            AssertExtension.Equal(expectedViewModel.GlobalThresholdModel, actualViewModel.GlobalThresholdModel);
            AssertExtension.Equal(expectedViewModel.MyWatchListAccountPagingViewModel, actualViewModel.MyWatchListAccountPagingViewModel);
            Assert.Equal(expectedViewModel.SearchAccountId, actualViewModel.SearchAccountId);
            Assert.Equal(expectedViewModel.IsMobile, actualViewModel.IsMobile);
            Assert.Equal(expectedViewModel.IsShowLastLoginColumn, actualViewModel.IsShowLastLoginColumn);
            Assert.Equal(expectedViewModel.CustLevel, actualViewModel.CustLevel);
            Assert.Equal(expectedViewModel.CanUpdateAccount, actualViewModel.CanUpdateAccount);
        }

        [Fact]
        public async Task HandleWatchlistAccountsQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new WatchlistAccountsQuery(user);
            var watchlistAccountReport = CreateWatchlistAccountReport();
            service.GetWatchlistAccounts(Arg.Any<SelectWatchlistAccountsParam>()).Returns(Task.FromResult(watchlistAccountReport));
            var expectedViewModel = new MyWatchlistAccountsPagingViewModel(watchlistAccountReport, message);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

       [Fact]
       public async Task HandlePersonalPresetQuery_Always_ReturnsCorrectPresets()
       {
            // Arrange
            var message = new PersonalPresetQuery(user);
            IEnumerable<PersonalPresetModel> expectedPresets = new List<PersonalPresetModel>()
            {
                new PersonalPresetModel(1, 0, "Preset1", 1000, 2000),
                new PersonalPresetModel(2, 0, "Preset2", 2000, 3000)
            };
            service.GetPresets(Arg.Any<SelectPresetsParam>()).Returns(Task.FromResult(expectedPresets));

            // Act
            var actualPresets = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedPresets, actualPresets);
       }

       [Fact]
       public async Task HandleAddPersonalThresholdCommand_Always_ReturnsCorrectResults()
       {
            // Arrange
            var message = new AddPersonalThresholdCommand(user);
            IEnumerable<NamedResult> expectedResults = new List<NamedResult>()
            {
                new NamedResult("agent0101", 0, "Add successfully"),
                new NamedResult("agent0102", 101, "Is not member"),
                new NamedResult("agent0102", 103, "Was added watchlist already"),
            };
            service.AddPersonalThreshold(Arg.Any<AddPersonalThresholdParam>()).Returns(Task.FromResult(expectedResults));

            // Act
            var actualResults = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResults, actualResults);
       }

        [Fact]
        public async Task HandleUpdateMultiplePersonalThresholdCommand_Always_ReturnsCorrectResults()
        {
            // Arrange
            var message = new UpdateMultiplePersonalThresholdCommand(user)
            {
                Usernames = "agent0101",
                CustIds = "1234"
            };
            var namedResult = new NamedResult("agent0101", 0, "Add successfully");
            service.UpdatePersonalThreshold(Arg.Any<UpdatePersonalThresholdParam>()).Returns(Task.FromResult(namedResult));
            IEnumerable<NamedResult> expectedResults = new List<NamedResult>()
            {
                namedResult
            };

            // Act
            var actualResults = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task HandleDeleteMultiplePersonalThresholdCommand_Always_ReturnsCorrectResults()
        {
            // Arrange
            var message = new DeleteMultiplePersonalThresholdCommand(user)
            {
                Usernames = "agent0101",
                CustIds = "1234"
            };
            var namedResult = new NamedResult("agent0101", 0, "Remove successfully");
            service.DeletePersonalThreshold(Arg.Any<DeletePersonalThresholdParam>()).Returns(Task.FromResult(namedResult));
            IEnumerable<NamedResult> expectedResults = new List<NamedResult>()
            {
                namedResult
            };

            // Act
            var actualResults = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResults, actualResults);
        }

        [Fact]
        public async Task HandleUpdateGlobalThresholdCommand_Always_ReturnsCorrectResults()
        {
            // Arrange
            var message = new UpdateGlobalThresholdCommand(user);
            var expectedResult = new Result() 
            {
                Code = 0,
                Message = "Update successfully"
            };
            service.UpdateGlobalThreshold(Arg.Any<UpdateGlobalThresholdParam>()).Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task HandleAddMultiplePersonalPresetCommand_Always_ReturnCorrectResults()
        {
            // Arrange
            var message = new AddMultiplePersonalPresetCommand(user)
            {
                Presets = new PresetInfo[]
                {
                    new PresetInfo()
                    {
                        PresetTitle = "Preset1",
                        WinningThreshold = 1000,
                        LosingThreshold = 200
                    }
                }
            };
            var namedResult = new NamedResult("Preset1", 0, "Add successfully");
            service.AddPersonalPreset(Arg.Any<AddPersonalPresetParam>()).Returns(Task.FromResult(namedResult));
            IEnumerable<NamedResult> expectedResults = new List<NamedResult>()
            {
                namedResult
            };

            // Act
            var actualResult = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResults, actualResult);
        }

        [Fact]
        public async Task HandleUpdatePersonalPresetCommand_Always_ReturnsCorrectResults()
        {
            // Arrange
            var message = new UpdatePersonalPresetCommand(user);
            var expectedResult = new Result()
            {
                Code = 0,
                Message = "Update successfully"
            };
            service.UpdatePersonalPreset(Arg.Any<UpdatePersonalPresetParam>()).Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task HandleDeletePersonalPresetCommand_Always_ReturnsCorrectResults()
        {
            // Arrange
            var message = new DeletePersonalPresetCommand(user);
            var expectedResult = new Result()
            {
                Code = 0,
                Message = "Delete successfully"
            };
            service.DeletePersonalPreset(Arg.Any<DeletePersonalPresetParam>()).Returns(Task.FromResult(expectedResult));

            // Act
            var actualResult = await handler.Handle(message);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task HandleManageWatchlistAccountsQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ManageWatchlistAccountsQuery(user);
            IEnumerable<PersonalPresetModel> presets = new List<PersonalPresetModel>()
            {
                new PersonalPresetModel(1234, 0, "Preset1", 1000, 2000),
                new PersonalPresetModel(1235, 0, "Preset2", 2000, 3000)
            };
            service.GetPresets(Arg.Any<SelectPresetsParam>()).Returns(Task.FromResult(presets));
            var expectedViewModel = new ManageWatchlistAccountsViewModel(message, presets);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public async Task HandleManageGlobalThresholdQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ManageGlobalThresholdQuery(user);
            var globalThreshold = new GlobalThresholdModel(1000, 2000);
            service.GetGlobalThreshold(Arg.Any<SelectGlobalThresholdParam>()).Returns(Task.FromResult(globalThreshold));
            var expectedViewModel = new ManageGlobalThresholdViewModel(globalThreshold);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public async Task HandleManagePersonalThresholdPresetQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ManagePersonalThresholdPresetQuery(user);
            IEnumerable<PersonalPresetModel> presets = new List<PersonalPresetModel>()
            {
                new PersonalPresetModel(1234, 0, "Preset1", 1000, 2000),
                new PersonalPresetModel(1235, 0, "Preset2", 2000, 3000)
            };
            service.GetPresets(Arg.Any<SelectPresetsParam>()).Returns(Task.FromResult(presets));
            var expectedViewModel = new ManagePersonalThresholdPresetViewModel(presets);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }
    }
}

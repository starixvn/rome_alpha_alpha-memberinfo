namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using NSubstitute;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class ReachedThresholdControllerTests
    {
        private readonly IMediator mediator;
        private readonly ReachedThresholdController controller;
        private readonly IUser user;

        public ReachedThresholdControllerTests()
        {
            mediator = Substitute.For<IMediator>();

            user = Substitute.For<IUser>();
            user.Id.Returns(1176086);
            user.Name.Returns("agent01");
            user.LevelId.Returns((byte)1);
            user.Language.Returns("en-US");

            controller = new ReachedThresholdController(mediator);
        }

        [Fact]
        public async Task Index_IsValid_CallsMediatorReachedThresholdQuery()
        {
            // Arrange
            var message = new ReachedThresholdQuery(user);

            // Act
            await controller.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task ReachHistoryIndex_IsValid_CallsMediatorReachedHistoryQuery()
        {
            // Arrange
            var message = new ReachHistoryQuery(user) 
            { 
                FromDate = DateTime.Today.AddDays(-1),
                ToDate = DateTime.Today
            };

            // Act
            await controller.ReachHistoryIndex(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task GetReachlistThreshold_IsValid_CallsMediatorReachlistAccountsQuery()
        { 
            // Arrange
            var message = new ReachlistAccountsQuery(user);

            // Act
            await controller.GetReachlistThreshold(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task GetReachlistThreshold_Always_CallsMediatorReachedThresholdQuery()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);
            mediator
               .SendAsync(Arg.Any<ReachedThresholdExcelQuery>())
               .Returns(new ExcelFileViewModel("Excel File", new byte[0]));

            // Act
            await controller.Excel(message);

            // Assert
            await mediator.Received().SendAsync(Arg.Is(message));
        }

        [Fact]
        public async Task GetReachHistoryList_IsValid_CallsMediatorReachHistoryListQuery()
        {
            // Arrange
            var message = new ReachHistoryListQuery(user)
            {
                FromDate = DateTime.Today.AddDays(-1),
                ToDate = DateTime.Today
            };

            // Act
            await controller.GetReachHistoryList(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }
    }
}

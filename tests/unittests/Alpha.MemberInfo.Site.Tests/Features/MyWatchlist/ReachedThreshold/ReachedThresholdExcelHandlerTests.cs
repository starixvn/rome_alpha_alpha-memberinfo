namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Extensions;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold;
    using Alpha.MemberInfo.Site.Features.MyWatchlist.ReachedThreshold.Requests;
    using NSubstitute;
    using ONEbook.SpreadSheets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class ReachedThresholdExcelHandlerTests
    {
        private readonly IUser user;
        private readonly ReachlistAccountReport reachlistAccountReport;
        private readonly ReachedThresholdService service;
        private readonly ReachedThresholdWorkbook reachedThresholdWorkbook;
        private readonly Workbook<ReachlistAccountModel> onebookWorkbook;
        private readonly ReachedThresholdExcelHandler handler;

        public ReachedThresholdExcelHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(1176086);
            user.Name.Returns("agent01");
            user.LevelId.Returns((byte)1);
            user.Language.Returns("en-US");

            service = Substitute.For<ReachedThresholdService>();
            reachlistAccountReport = new ReachlistAccountReport(Enumerable.Empty<ReachlistAccountModel>(), 0);
            service.GetReachlistAccounts(Arg.Any<SelectReachListParam>()).Returns(reachlistAccountReport);

            reachedThresholdWorkbook = Substitute.For<ReachedThresholdWorkbook>();
            onebookWorkbook = Substitute.For<Workbook<ReachlistAccountModel>>();

            reachedThresholdWorkbook.SetData(Arg.Any<IEnumerable<ReachlistAccountModel>>()).Returns(reachedThresholdWorkbook);
            reachedThresholdWorkbook.SetRowMappers(Arg.Any<Mapper<ReachlistAccountModel>>()).Returns(reachedThresholdWorkbook);
            reachedThresholdWorkbook.UseTemplate(Arg.Any<TemplateInfo>()).Returns(onebookWorkbook);

            onebookWorkbook.Sheet(Arg.Any<string>()).Returns(onebookWorkbook);
            onebookWorkbook.Title(Arg.Any<string>()).Returns(onebookWorkbook);
            onebookWorkbook.Header(Arg.Any<Func<string, string>>()).Returns(onebookWorkbook);

            handler = Substitute.ForPartsOf<ReachedThresholdExcelHandler>(service, reachedThresholdWorkbook);
            handler.MapPath = path => path;
            handler.IsFileExist = path => true;
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_Always_CallsGetReachlistAccounts()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            await service.Received().GetReachlistAccounts(Arg.Any<SelectReachListParam>());
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_IsNotHaveLimitedDate_UsesReachedThresholdTemplateInfo()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            handler.Received().GetExcelTemplatePath("ReachedThreshold");
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_IsHaveLimitedDate_UsesReachedThresholdHideDateTimeTemplateInfo()
        {
            // Arrange
            user.LimitedDateToShowInfo.Returns(new DateTime(2022, 1, 1));
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            handler.Received().GetExcelTemplatePath("ReachedThresholdHideDateTime");
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_Always_ExportsCorrectTitle()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user)
            { 
                FromDate = DateTime.Today.AddDays(-1),
                ToDate = DateTime.Today,
                Status = 0,
                LimitType = 0,
                Username = "agent0101"
            };
            var expectedTitle = new StringBuilder();
            expectedTitle.Append("Reached Threshold - ");
            expectedTitle.Append($"From: {message.FromDate.ToShortDateWithSlash()} - ");
            expectedTitle.Append($"To: {message.ToDate.ToShortDateWithSlash()} - ");
            expectedTitle.Append("Status: All - Threshold Type: All - Username: agent0101");

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Title(Arg.Is(expectedTitle.ToString()));
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_Always_ExportsCorrectHeader()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Header(Arg.Any<Func<string, string>>());
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_Always_ExportsCorrectSheetName()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Sheet(Arg.Is("Sheet 1"));
        }

        [Fact]
        public async Task HandleReachedThresholdExcelQuery_Always_ExportsExcelToBytes()
        {
            // Arrange
            var message = new ReachedThresholdExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().ExportToBytes();
        }

    }
}

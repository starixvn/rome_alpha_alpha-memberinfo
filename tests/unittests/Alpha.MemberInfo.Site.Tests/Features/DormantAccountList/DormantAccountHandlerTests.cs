namespace Alpha.MemberInfo.Site.Tests.Features.DormantAccountList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.DormantAccountList;
    using Alpha.MemberInfo.Features.DormantAccountList.Models;
    using Alpha.MemberInfo.Features.DormantAccountList.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.DormantAccountList;
    using Alpha.MemberInfo.Site.Features.DormantAccountList.Requests;
    using NSubstitute;
    using ONEbook.SpreadSheets;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class DormantAccountHandlerTests
    {
        private readonly IUser user;
        private readonly DormantAccountReport report;
        private readonly DormantAccountService service;
        private readonly DormantAccountWorkbook dormantAccountWorkbook;
        private readonly Workbook<DormantAccount> onebookWorkbook;
        private readonly DormantAccountHandler handler;

        public DormantAccountHandlerTests()
        {
            user = Substitute.For<IUser>();

            service = Substitute.For<DormantAccountService>();
            report = new DormantAccountReport(Enumerable.Empty<DormantAccount>(), 0);
            service
                .GetDormantAccountsAsync(Arg.Any<DormantAccountParam>())
                .Returns(report);

            dormantAccountWorkbook = Substitute.For<DormantAccountWorkbook>();
            onebookWorkbook = Substitute.For<Workbook<DormantAccount>>();

            dormantAccountWorkbook
                .SetData(Arg.Any<IEnumerable<DormantAccount>>(), Arg.Any<DormantAccount>())
                .Returns(dormantAccountWorkbook);
            dormantAccountWorkbook
                .SetRowMappers(Arg.Any<Mapper<DormantAccount>>(), Arg.Any<Mapper<DormantAccount>>())
                .Returns(dormantAccountWorkbook);
            dormantAccountWorkbook.UseTemplate(Arg.Any<TemplateInfo>()).Returns(onebookWorkbook);

            onebookWorkbook.Title(Arg.Any<string>()).Returns(onebookWorkbook);
            onebookWorkbook.Header(Arg.Any<Func<string, string>>()).Returns(onebookWorkbook);
            onebookWorkbook.Footer(Arg.Any<DormantAccount>(), Arg.Any<Mapper<DormantAccount>>()).Returns(onebookWorkbook);
            onebookWorkbook.Sheet(Arg.Any<string>()).Returns(onebookWorkbook);

            handler = Substitute.ForPartsOf<DormantAccountHandler>(service, dormantAccountWorkbook);
            handler.MapPath = (path) => path;
            handler.IsFileExist = (path) => true;
        }

        [Fact]
        public async Task HandleDormantAccountQuery_Always_CallsGetDormantAccountsAsync()
        {
            // Arrange
            var message = new DormantAccountQuery(user)
            {
                Username = "master1",
                LevelId = 3,
                StatusId = 4,
                PageIndex = 2,
                PageSize = 10
            };

            // Act
            await handler.Handle(message);

            // Assert
            await service.Received().GetDormantAccountsAsync(Arg.Is<DormantAccountParam>(
                i => i.Username == "master1"
                && i.RoleId == 3
                && i.StatusId == 4
                && i.PageIndex == 2
                && i.PageSize == 10));
        }

        [Fact]
        public async Task HandleDormantAccountExcelQuery_Always_UsesCorrectTemplateInfo()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            dormantAccountWorkbook.Received().UseTemplate(Arg.Is<TemplateInfo>(tp => tp.NoInfomationText == "No information is available"));
        }

        [Fact]
        public async Task HandleDormantAccountExcelQuery_Always_ExportsCorrectTitle()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Title(Arg.Is($"Dormant Account List {DateTime.Today.ToString("MM/dd/yyyy")}"));
        }

        [Fact]
        public async Task HandleDormantAccountExcelQuery_Always_ExportsCorrectHeader()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Header(Arg.Any<Func<string, string>>());
        }

        [Fact]
        public async Task HandleDormantAccountExcelQuery_Always_ExportsCorrectSheetName()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().Sheet(Arg.Is("Dormant Account List"));
        }

        [Fact]
        public async Task HandleDormantAccountExcelQuery_Always_ExportsExcelToBytes()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);

            // Act
            await handler.Handle(message);

            // Assert
            onebookWorkbook.Received().ExportToBytes();
        }
    }
}

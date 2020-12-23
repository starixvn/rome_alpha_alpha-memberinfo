namespace Alpha.MemberInfo.Site.Tests.Features.DormantAccountList
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.DormantAccountList;
    using Alpha.MemberInfo.Site.Features.DormantAccountList.Requests;
    using Alpha.MemberInfo.Site.ViewModels;
    using MediatR;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class DormantAccountControllerTests
    {
        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly DormantAccountController controller;

        public DormantAccountControllerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(1176070);
            user.Name.Returns("supertest1");
            user.LevelId.Returns((byte)4);
            user.Language.Returns("en-US");

            mediator = Substitute.For<IMediator>();
            controller = new DormantAccountController(mediator);
        }

        [Fact]
        public async Task Index_ValidQuery_CallsMediatorDormantAccountQuery()
        {
            // Arrange
            var message = new DormantAccountQuery(user);

            // Act
            await controller.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task Index_InvalidQuery_ReturnsInvalidPage()
        {
            // Arrange
            user.Id.Returns(-1176070);
            var message = new DormantAccountQuery(user);

            // Act
            var actualView = await controller.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/InvalidPage.cshtml", actualView.ViewName);
        }

        [Fact]
        public async Task Excel_ValidQuery_CallsMediatorDormantAccountExcelQuery()
        {
            // Arrange
            var message = new DormantAccountExcelQuery(user);
            mediator
                .SendAsync<ExcelFileViewModel>(Arg.Any<DormantAccountExcelQuery>())
                .Returns(new ExcelFileViewModel("Excel File", new byte[0]));

            // Act
            await controller.Excel(message);

            // Assert
            await mediator.Received().SendAsync<ExcelFileViewModel>(Arg.Is(message));
        }

        [Fact]
        public async Task Excel_InvalidQuery_ReturnInvalidReportExcelFile()
        {
            // Arrange
            user.Id.Returns(-1176070);
            var message = new DormantAccountExcelQuery(user);

            // Act
            var actualView = await controller.Excel(message) as FileContentResult;

            // Assert
            Assert.Equal("Invalid.xls", actualView.FileDownloadName);
        }
    }
}

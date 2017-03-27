////namespace Alpha.MemberInfo.Site.Tests.Controllers
////{
////    using System.Web.Mvc;
////    using Fixtures;
////    using MediatR;
////    using NSubstitute;
////    using Reports.Models;
////    using Site.Controllers;
////    using Site.Requests.WinLoss;
////    using Site.ViewModels;
////    using Xunit;

////    [Trait("Category", "MemberRequests")]
////    [Collection("UserCollection")]
////    public class MemberRequestsControllerTests
////    {
////        private readonly IMediator mediator;
////        private readonly EnteredResultController enteredResultController;
////        private readonly UserFixture userFixture;

////        public MemberRequestsControllerTests(UserFixture userFixture)
////        {
////            mediator = Substitute.For<IMediator>();
////            enteredResultController = new MemberRequestsController(mediator);
////            this.userFixture = userFixture;
////        }

////        [Fact]
////        public void EnteredResult_Always_CallsMediatorSendEnteredResultQuery()
////        {
////            // Arrange
////            var message = new EnteredSportResultQuery(userFixture.User);

////            // Act
////            enteredResultController.Index(message);

////            // Assert
////            mediator.Received().Send(message);
////        }

////        [Fact]
////        public void EnteredResultExcel_Always_CallsMediatorSendExportEnteredResultQuery()
////        {
////            // Arrange
////            var message = new EnteredSportResultExcelQuery(userFixture.User);
////            mediator.Send<ExcelFileViewModel>(Arg.Any<EnteredSportResultExcelQuery>()).Returns(new ExcelFileViewModel("Excel File", new byte[0]));

////            // Act
////            enteredResultController.Excel(message);

////            // Assert
////            mediator.Received().Send<ExcelFileViewModel>(Arg.Is(message));
////        }

////        [Fact]
////        public void Index_QueryIsNotValid_ReturnInvalidReportView()
////        {
////            // Arrange
////            var user = Substitute.For<IUser>();
////            user.LevelId.Returns((byte)2);
////            var message = new EnteredSportResultQuery(user);

////            // Act
////            var actualView = enteredResultController.Index(message) as ViewResult;

////            // Assert
////            Assert.Equal("~/Views/Shared/InvalidReport.cshtml", actualView.ViewName);
////        }

////        [Fact]
////        public void Excel_QueryIsNotValid_ReturnInvalidReportExcelFile()
////        {
////            // Arrange
////            var user = Substitute.For<IUser>();
////            user.LevelId.Returns((byte)2);
////            var message = new EnteredSportResultExcelQuery(user);

////            // Act
////            var actualView = enteredResultController.Excel(message) as FileContentResult;

////            // Assert
////            Assert.Equal("InvalidReport.xls", actualView.FileDownloadName);
////        }
////    }
////}
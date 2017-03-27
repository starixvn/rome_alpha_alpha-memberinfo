////namespace Alpha.MemberInfo.Site.Tests.Controllers
////{
////    using System.Web.Mvc;
////    using MediatR;
////    using MemberInfo.Models;
////    using MemberInfo.Tests.Fixtures;
////    using NSubstitute;
////    using Requests.MemberRequest;
////    using Site.Controllers;
////    using Xunit;

////    [Trait("Category", "MemberRequests")]
////    [Collection("UserCollection")]
////    public class MemberRequestsControllerTests
////    {
////        private readonly IMediator mediator;
////        private readonly MemberRequestsController controller;
////        private readonly UserFixture userFixture;

////        public MemberRequestsControllerTests(UserFixture userFixture)
////        {
////            mediator = Substitute.For<IMediator>();
////            controller = new MemberRequestsController(mediator);
////            this.userFixture = userFixture;
////        }

////        [Fact]
////        public void Index_Always_CallsMediatorProductQuery()
////        {
////            // Arrange
////            var message = new ProductQuery(userFixture.User);

////            // Act
////            controller.Index(message);

////            // Assert
////            mediator.Received().Send(message);
////        }

////        [Fact]
////        public void Index_QueryIsNotValid_ReturnInvalidPageView()
////        {
////            // Arrange
////            var user = Substitute.For<IUser>();
////            user.LevelId.Returns((byte)2);
////            var message = new ProductQuery(user);

////            // Act
////            var actualView = controller.Index(message) as ViewResult;

////            // Assert
////            Assert.Equal("~/Views/Shared/InvalidReport.cshtml", actualView.ViewName);
////        }
////    }
////}
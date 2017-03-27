namespace Alpha.MemberInfo.Site.Tests.Handlers.MemberRequests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Constants;
    using Fixtures;
    using MemberInfo.Models.MemberRequests;
    using NSubstitute;
    using Params;
    using Requests.MemberRequest;
    using Services;
    using Site.Handlers.MemberRequest;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    [Collection("UserCollection")]
    public class MemberRequestsHandlerTests
    {
        private readonly MemberRequestService service;
        private readonly MemberRequestHandler handler;

        public MemberRequestsHandlerTests(UserFixture userFixture)
        {
            service = Substitute.For<MemberRequestService>();
            handler = new MemberRequestHandler(service);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        [Fact]
        public void HandlerProductQuery_NullMessage_ThrowsArgumentNullException()
        {
            // Arrange
            ProductQuery nullMessage = null;

            // Act &  Assert
            Assert.Throws<ArgumentNullException>(() => handler.Handle(nullMessage));
        }

        [Fact]
        public void HandlerProductQuery_CallSupportedProductService()
        {
            // Arrange
            int custId = 12;

            // Act
            service.SupportedProduct(custId);

            // Assert
            service
                .Received()
                .SupportedProduct(Arg.Is<int>(inputCustId => inputCustId.Equals(custId)));
        }

        [Fact]
        public void HandlerMemberRequestsQuery_NullMessage_ThrowsArgumentNullException()
        {
            // Arrange
            MemberRequestsQuery nullMessage = null;

            // Act &  Assert
            Assert.Throws<ArgumentNullException>(() => handler.Handle(nullMessage));
        }

        [Fact]
        public void HandlerMemberRequestsQuery_CallGetMemberRequestsService()
        {
            // Arrange
            MemberRequestsParam param = new MemberRequestsParam(12, 6);

            // Act
            service.GetMemberRequest(param);

            // Assert
            service
                .Received()
                .GetMemberRequest(Arg.Is<MemberRequestsParam>(
                    inputParam => inputParam.CustId.Equals(param.CustId)
                    && inputParam.ProductId.Equals(param.ProductId)));
        }

        [Fact]
        public void HandlerEnableProductCommand_NullMessage_ThrowsArgumentNullException()
        {
            // Arrange
            EnableProductCommand nullMessage = null;

            // Act &  Assert
            Assert.Throws<ArgumentNullException>(() => handler.Handle(nullMessage));
        }

        [Fact]
        public void HandlerEnableProductCommand_CallEnableProductService()
        {
            // Arrange
            var param = new EnableProductParam(
                productId: 6,
                members: Enumerable.Empty<MemberRequestModel>(),
                actorId: 16,
                actorName: "Username",
                subAccId: 0,
                loginIP: "192.168.1.1",
                language: "E");

            // Act
            service.EnableProduct(param);

            // Assert
            service
                .Received()
                .EnableProduct(Arg.Is<EnableProductParam>(
                    inputParam => inputParam.ProductId.Equals(param.ProductId)
                    && inputParam.ActorId.Equals(param.ActorId)
                    && inputParam.ActorName.Equals(param.ActorName)
                    && inputParam.SubAccId.Equals(param.SubAccId)
                    && inputParam.LoginIP.Equals(param.LoginIP)
                    && inputParam.Language.Equals(param.Language)));
        }
    }
}
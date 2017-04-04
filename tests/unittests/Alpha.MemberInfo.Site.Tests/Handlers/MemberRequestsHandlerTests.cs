namespace Alpha.MemberInfo.Site.Tests.Handlers.MemberRequests
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Constants;
    using Fixtures;
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

        public MemberRequestsHandlerTests(UserFixture userFixture)
        {
            service = Substitute.For<MemberRequestService>();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        [Fact]
        public void HandlerProductQuery_CallSupportedProductService()
        {
            // Arrange
            int custId = 12;

            // Act
            service.GetPlayForFunProduct(custId);

            // Assert
            service
                .Received()
                .GetPlayForFunProduct(Arg.Is<int>(inputCustId => inputCustId.Equals(custId)));
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
    }
}
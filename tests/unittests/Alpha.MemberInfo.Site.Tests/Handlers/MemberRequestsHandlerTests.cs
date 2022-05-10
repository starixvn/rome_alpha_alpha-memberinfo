namespace Alpha.MemberInfo.Site.Tests.Handlers.MemberRequests
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Constants;
    using Extensions;
    using Fixtures;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;
    using NSubstitute;
    using Params;
    using Services.MemberRequest;
    using Site.Handlers.MemberRequest;
    using Site.Requests.MemberRequest;
    using Site.ViewModels;
    using Site.ViewModels.MemberRequest;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    [Collection("UserCollection")]
    public class MemberRequestsHandlerTests
    {
        private readonly MemberRequestService service;
        private readonly MemberRequestHandler handler;
        private readonly IUser user;
        private readonly GetSettingResult setting;

        public MemberRequestsHandlerTests(UserFixture userFixture)
        {
            user = userFixture.User;
            service = Substitute.For<MemberRequestService>();
            setting = new GetSettingResult(true, 0, string.Empty);
            service.GetSetting(Arg.Any<ProductSettingParam>()).Returns(setting);
            service.GetMemberRequest(Arg.Any<MemberRequestsParam>()).Returns(Enumerable.Empty<MemberRequestModel>());
            service.IsSync(Arg.Any<int>()).Returns(true);

            handler = new MemberRequestHandler(service);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        
        [Fact]
        public void HandlerProductQuery_EmptyProduct_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ProductQuery(user);

            service.GetPlayForFunProduct(Arg.Any<int>()).Returns(Enumerable.Empty<Product>());

            var expectedViewModel = new ProductsViewModel(
                message.ViewerId,
                message.UserSelectedProductId,
                Enumerable.Empty<ProductViewModel>());

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandlerProductQuery_NullProduct_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ProductQuery(user);

            IEnumerable<Product> products = null;
            service.GetPlayForFunProduct(Arg.Any<int>()).Returns(products);

            var expectedViewModel = new ProductsViewModel(
                message.ViewerId,
                message.UserSelectedProductId,
                Enumerable.Empty<ProductViewModel>());

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandlerMemberRequestsQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new MemberRequestsQuery(user)
            {
                UserSelectedProductId = ProductId.MacauGames,
                DisabledUserSelectedProduct = false,
                CustId = 1
            };

            var expectedViewModel = new MemberRequestViewModel(
                Enumerable.Empty<MemberRequestModel>(),
                ProductId.MacauGames,
                false,
                1,
                message.CanUpdateAccount);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }
    }
}
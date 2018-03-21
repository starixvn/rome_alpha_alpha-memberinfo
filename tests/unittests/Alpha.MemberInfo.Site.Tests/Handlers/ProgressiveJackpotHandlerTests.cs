namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Fixtures;
    using MemberInfo.Constants;
    using MemberInfo.Extensions;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;
    using MemberInfo.Models.ProgressiveJackpot;
    using NSubstitute;
    using Params;
    using Requests.ProgressiveJackpot;
    using Services.ProgressiveJackpot;
    using Site.Handlers;
    using ViewModels;
    using ViewModels.ProgressiveJackpot;
    using Xunit;

    [Collection("UserCollection")]
    [Trait("Category", "ProgressiveJackpot")]
    public class ProgressiveJackpotHandlerTests
    {
        private readonly UserFixture userFixture;
        private readonly ProgressiveJackpotService progressiveJackpotService;
        private readonly ProgressiveJackpotHandler handler;

        public ProgressiveJackpotHandlerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            progressiveJackpotService = Substitute.For<ProgressiveJackpotService>();
            handler = new ProgressiveJackpotHandler(progressiveJackpotService);
        }

        [Fact]
        public void Handle_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ProgressiveJacpotQuery(userFixture.User);
            message.UserSelectedProductIds = "6";
            var products = new List<Product>
            {
                new Product(ProductId.Casino,"Casino",false)
            };
            progressiveJackpotService.FilterProductIds(Arg.Any<IEnumerable<int>>()).Returns(new List<int> { 6 });
            progressiveJackpotService.GetJackpotProducts(Arg.Any<IEnumerable<int>>()).Returns(products);
            progressiveJackpotService.GetJackpotGameSettings(Arg.Any<ProgressiveJackpotParam>()).Returns(new List<JackpotGameSetting>());
            var expectedViewModel = new ProgressiveJackpotViewModel(message, new List<JackpotGameSetting>(), handler.BuildProductsViewModel(message.UserSelectedProductIds.ToListInt(), products));

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void BuildProductsViewModel_NullProducts_ReturnsEmptyProductViewModelList()
        {
            // Arrange
            IEnumerable<Product> products = null;
            var expectedProductViewModelList = Enumerable.Empty<ProductViewModel>();

            // Act
            var actualProductViewModelList = handler.BuildProductsViewModel(Enumerable.Empty<int>(), products);

            // Assert
            AssertExtension.Equal(expectedProductViewModelList, actualProductViewModelList);
        }

        [Fact]
        public void BuildProductsViewModel_EmptyProductList_ReturnsEmptyProductViewModelList()
        {
            // Arrange
            IEnumerable<Product> products = Enumerable.Empty<Product>();
            var expectedProductViewModelList = Enumerable.Empty<ProductViewModel>();

            // Act
            var actualProductViewModelList = handler.BuildProductsViewModel(Enumerable.Empty<int>(), products);

            // Assert
            AssertExtension.Equal(expectedProductViewModelList, actualProductViewModelList);
        }

        [Fact]
        public void BuildProductsViewModel_HasDataProductList_ReturnsCorrectProductViewModelList()
        {
            // Arrange
            IEnumerable<Product> products = new List<Product>
            {
                new Product(6,"Casino",false)
            };

            IEnumerable<ProductViewModel> expectedProductViewModelList = new List<ProductViewModel>
            {
                new ProductViewModel("Casino","6",true,false,new GetSettingResult(false,0,string.Empty))
            };

            // Act
            var actualProductViewModelList = handler.BuildProductsViewModel(new List<int> { 6 }, products).ToList();

            // Assert
            AssertExtension.Equal(expectedProductViewModelList, actualProductViewModelList);
        }
    }
}
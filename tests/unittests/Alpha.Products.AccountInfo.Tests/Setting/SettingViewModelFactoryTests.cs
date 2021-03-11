namespace Alpha.Products.AccountInfo.Setting
{
    using System;
    using System.Linq;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Services;
    using NSubstitute;
    using Xunit;
    using static Alpha.Products.Core.Shared.ProductConstants;
    using static Tests.Utility.AppProductConfigurationHelper;
    using static Tests.Utility.CustomerHelper;

    [Trait("Setting", "SettingViewModelFactory")]
    public class SettingViewModelFactoryTests
    {
        private readonly SettingViewModelFactory factory;
        private readonly IConfigurationLoader configurationLoader;

        public SettingViewModelFactoryTests()
        {
            configurationLoader = Substitute.For<IConfigurationLoader>();
            configurationLoader.Load().Returns(LoadApplicationProductConfiguration());

            SimpleServiceLocator
               .Instance
               .Register<IConfigurationLoader>(configurationLoader)
               .Register<Func<int, string>>((productId) => $"Product Name of {productId}")
               .Register<Func<string, string>>(key => $"value of {key}")
               .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            factory = new SettingViewModelFactory();
        }

        //[Theory]
        //[InlineData(Sportsbook, typeof(SportsbookViewModel))]
        //[InlineData(RNGCasino, typeof(RNGCasinoViewModel))]
        //[InlineData(Keno, typeof(KenoViewModel))]
        //[InlineData(GoldDeluxe, typeof(GoldDeluxeViewModel))]
        //[InlineData(ColossusBets, typeof(ColossusBetsViewModel))]
        //[InlineData(24, typeof(DynamicProductViewModel))]
        //[InlineData(26, typeof(DynamicProductViewModel))]
        //[InlineData(28, typeof(DynamicProductViewModel))]
        //public void GetViewModels_Returns_RightMappingProductViewModel(int productId, Type viewModelType)
        //{
        //    var request = new SettingSearchAccountRequest(CreateSuper(), CreateMaster());

        //    var viewModels = factory.GetViewModels(request);

        //    var rightModelMapping = viewModels
        //         .Any(viewModel => viewModel is ProductSettingItemViewModel productViewModel
        //         && productViewModel.ProductId == productId && productViewModel.GetType() == viewModelType);

        //    Assert.True(rightModelMapping);
        //}
    }
}
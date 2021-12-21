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
    }
}
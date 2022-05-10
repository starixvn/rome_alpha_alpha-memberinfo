namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public static class AppProductConfigurationHelper
    {
        public static IList<IConfigurationItem> LoadApplicationProductConfiguration()
        => new List<IConfigurationItem>()
        {
                new ConfigurationItem(1, "Sportsbook", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(3, "Number Game", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(5, "Virtual Sports", extendProductProperty: new SearchAccountProperties(true, true, true)),              
                new ConfigurationItem(8, "Keno", extendProductProperty: new SearchAccountProperties(true, true, true)),            
                new ConfigurationItem(16, "Sportsbook2", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(41, "Live Casino", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(22, "Macau Games", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(23, "NG2", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(24, "Fishing World", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(44, "RNG Lottery", extendProductProperty: new SearchAccountProperties(true, true, true))
        };

        public static IList<IConfigurationItem> LoadApplicationProductConfiguration(params int[] productIds)
        {
            var configurationItems = new List<IConfigurationItem>();

            foreach (var productId in productIds)
            {
                configurationItems.Add(new ConfigurationItem(productId, $"productName {productId}"));
            }

            return configurationItems;
        }
    }
}
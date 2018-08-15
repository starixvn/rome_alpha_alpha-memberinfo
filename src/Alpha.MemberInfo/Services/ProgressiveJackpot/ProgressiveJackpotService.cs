namespace Alpha.MemberInfo.Services.ProgressiveJackpot
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using CasinoLibrary.SBAgent;
    using Constants;
    using Fanex.Common;
    using Models;
    using Models.ProgressiveJackpot;
    using Params;

    public interface ProgressiveJackpotService
    {
        IEnumerable<int> FilterProductIds(IEnumerable<int> productIds);

        IEnumerable<Product> GetJackpotProducts(IEnumerable<int> productIds);

        IEnumerable<JackpotGameSetting> GetJackpotGameSettings(ProgressiveJackpotParam param);
    }

    public class ProgressiveJackpotServiceImpl : ProgressiveJackpotService
    {
        private const string DefaultLanguage = "En";

        private static readonly Dictionary<string, string> LanguageCodeMapping = new Dictionary<string, string>
        {
            ["en-US"] = DefaultLanguage,
            ["zh-TW"] = "Ct",
            ["zh-CN"] = "Cs",
            ["ja-JP"] = "Jp",
            ["ko-KR"] = "Ko",
            ["th-TH"] = "Th"
        };

        private readonly CacheRefService cacheRefService;
        private readonly Func<int, bool> canViewProduct;

        public ProgressiveJackpotServiceImpl(CacheRefService cacheRefService, Func<int, bool> canViewProduct)
        {
            this.cacheRefService = cacheRefService;
            this.canViewProduct = canViewProduct;
        }

        public IEnumerable<int> FilterProductIds(IEnumerable<int> productIds)
            => productIds.Where(id => canViewProduct(id));

        public IEnumerable<Product> GetJackpotProducts(IEnumerable<int> productIds)
        {
            var products = new List<Product>();

            foreach (int productId in productIds)
            {
                products.Add(new Product(Converter.To<byte>(productId), cacheRefService.GetProductName(productId), false));
            }

            return products;
        }

        public IEnumerable<JackpotGameSetting> GetJackpotGameSettings(ProgressiveJackpotParam param)
        {
            var result = new List<JackpotGameSetting>();

            if (param.ProductIds.Contains(ProductId.Casino))
            {
                result.AddRange(GetOneRNGJackpotSettings(param));
            }

            return result;
        }

        private static IEnumerable<JackpotGameSetting> GetOneRNGJackpotSettings(ProgressiveJackpotParam param)
        {
            var customer = new Customer();
            var languageCode = GetLanguageCode(param.Language);
            var gameSettings = customer.GetCustPJGameTypeSettings(param.CustId);

            if (gameSettings == null || gameSettings.Rows.Count == 0)
            {
                return Enumerable.Empty<JackpotGameSetting>();
            }

            var result = new List<JackpotGameSetting>();
            foreach (DataRow row in gameSettings.Rows)
            {
                result.Add(
                    new JackpotGameSetting(
                        Converter.ToString2(row["GameTypeName_" + languageCode]),
                        Converter.ToDecimal(row["CBRate"]),
                        Converter.ToDecimal(row["BetStake"])));
            }

            return result;
        }

        private static string GetLanguageCode(string language)
            => LanguageCodeMapping.ContainsKey(language)
            ? LanguageCodeMapping[language]
            : DefaultLanguage;
    }
}
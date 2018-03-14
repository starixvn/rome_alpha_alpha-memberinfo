namespace Alpha.MemberInfo.Services.ProgressiveJackpot
{
    using System.Collections.Generic;
    using System.Data;
    using CasinoLibrary.SBAgent;
    using Fanex.Common;
    using Models;
    using Models.ProgressiveJackpot;
    using Params;
    using RemotingRef;

    public interface ProgressiveJackpotService
    {
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

        public ProgressiveJackpotServiceImpl(CacheRefService cacheRefService)
        {
            this.cacheRefService = cacheRefService;
        }

        public IEnumerable<JackpotGameSetting> GetJackpotGameSettings(ProgressiveJackpotParam param)
        {
            var result = new List<JackpotGameSetting>();
            var customer = new Customer();

            var languageCode = GetLanguageCode(param.Language);
            var gameSettings = customer.GetCustPJGameTypeSettings(param.CustId);

            if (gameSettings != null && gameSettings.Rows.Count > 0)
            {
                foreach (DataRow row in gameSettings.Rows)
                {
                    result.Add(
                        new JackpotGameSetting(
                            Converter.ToString2(row["GameTypeName_" + languageCode]),
                            Converter.ToDecimal(row["CBRate"]),
                            Converter.ToDecimal(row["BetStake"])));
                }
            }

            return result;
        }

        public IEnumerable<Product> GetJackpotProducts(IEnumerable<int> productIds)
        {
            var products = new List<Product>();

            foreach (int productId in productIds)
            {
                products.Add(new Product(Converter.To<byte>(productId), cacheRefService.GetProductName(productId), false));
            }

            return products;
        }

        private static string GetLanguageCode(string language)
            => LanguageCodeMapping.ContainsKey(language)
            ? LanguageCodeMapping[language]
            : DefaultLanguage;
    }
}
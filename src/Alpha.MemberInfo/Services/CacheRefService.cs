namespace Alpha.MemberInfo.Services
{
    using System.Threading;
    using Fanex.RemotingRef;

    public interface CacheRefService
    {
        string GetBetTypeName(int betTypeId);

        string GetProductName(int productId);

        string GetSportTypeName(int sportTypeId);
    }

    public class CacheRefServiceImpl : CacheRefService
    {
        private readonly IRemotingRefService remotingRefService;

        public CacheRefServiceImpl(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }
        public string GetBetTypeName(int betTypeId)
                  => remotingRefService.GetBetTypeName(betTypeId.ToString(), LanguageCode);

        public string GetProductName(int productId)
            => remotingRefService.GetResourceDataByRefIdAndResourceId(
                "Product",
                productId.ToString(),
                LanguageCode,
                string.Empty);

        public string GetSportTypeName(int sportTypeId)
           => remotingRefService.GetSportName(sportTypeId.ToString(), LanguageCode);

        private static string LanguageCode
            => Thread.CurrentThread.CurrentCulture.Name;
    }
}

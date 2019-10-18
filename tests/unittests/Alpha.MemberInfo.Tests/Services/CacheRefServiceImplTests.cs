namespace Alpha.MemberInfo.Tests.Services
{
    using Fanex.RemotingRef;
    using MemberInfo.Services;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class CacheRefServiceImplTests
    {
        private readonly IRemotingRefService remotingRefService;
        private readonly CacheRefService cacheRefService;

        public CacheRefServiceImplTests()
        {
            remotingRefService = Substitute.For<IRemotingRefService>();
            cacheRefService = new CacheRefServiceImpl(remotingRefService);
        }
    }
}

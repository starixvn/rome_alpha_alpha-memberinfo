using Newtonsoft.Json;
using Xunit;

namespace Alpha.MemberInfo.Tests.Extensions
{
    public static class AssertExtension
    {
        public static void Equal<T>(T expected, T actual)
        {
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }
    }
}

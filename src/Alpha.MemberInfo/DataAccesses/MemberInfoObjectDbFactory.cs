namespace Alpha.MemberInfo.DataAccesses
{
    using System;
    using Fanex.Data;

    public static class MemberInfoObjectDbFactory
    {
        [ThreadStatic]
        private static Func<string, IObjectDb> createInstance;

        public static Func<string, IObjectDb> CreateInstance
        {
            get { return createInstance ?? ((settingKey) => ObjectDbFactory.CreateInstance(settingKey)); }
            internal set { createInstance = value; }
        }
    }
}

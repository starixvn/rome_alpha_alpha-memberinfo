
using Alpha.MemberInfo.Configurations;
using Fanex.Data;

namespace Alpha.MemberInfo.Site.Utilities
{
    public static class DbSettingProviderManagerExtension
    {   
        public static DbSettingProviderManager UseShardDbSettingProvider(this DbSettingProviderManager dbSettingProviderManager)
            => dbSettingProviderManager.Use(new ShardDbSettingProvider(new ShardInfoAccessor(),
                    resourcePath: AppSettings.StoreProcedurePath));

    }
}
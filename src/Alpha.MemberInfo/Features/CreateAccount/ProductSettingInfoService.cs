namespace Alpha.MemberInfo.Features.CreateAccount
{
    using Alpha.MemberInfo.Features.CreateAccount.Criteria;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.CreateAccount.Models;
    using Fanex.Data.Repository;
    using Fanex.Caching;
    using Alpha.MemberInfo.Configurations;

    public interface IProductSettingInfoService
    {
        Task<IEnumerable<ProductSettingInfoModel>> GetProductsSettingInfo(int cusId);
    }

    public class ProductSettingInfoService : IProductSettingInfoService
    {
        private readonly IDynamicRepository repository;

        public ProductSettingInfoService(IDynamicRepository dynamicRepository, ICacheService cacheService)
        {
            this.repository = dynamicRepository ?? new DynamicRepository();
        }

        public async Task<IEnumerable<ProductSettingInfoModel>> GetProductsSettingInfo(int cusId)
        {
            return await repository.FetchAsync<ProductSettingInfoModel>
            (
                new ProductSettingInfoCriteria
                {
                    CreationStatus = AppSettings.CreationStatus,
                    CustId = cusId
                }
            );
        }
    }
}
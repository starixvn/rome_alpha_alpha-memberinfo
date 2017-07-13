namespace Alpha.MemberInfo.Services.MemberRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequests;
    using Fanex.Data.Repository;
    using Models;
    using Models.MemberRequests;
    using Params;

    public interface MemberRequestService
    {
        IEnumerable<Product> GetPlayForFunProduct(int custId);

        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        GetSettingResult GetSetting(ProductSettingParam productSettingParam);

        bool IsSync(int productId);
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly IDynamicRepository repository;
        private readonly IUser user;
        private readonly Dictionary<int, Func<ProductSettingParam, GetSettingResult>> getSettingFuncs;

        public MemberRequestServiceImpl(
            IDynamicRepository dynamicRepository,
            IUser user,
            CasinoCustomer casinoCustomer)
        {
            this.user = user;
            repository = dynamicRepository;

            getSettingFuncs = new Dictionary<int, Func<ProductSettingParam, GetSettingResult>>
            {
                [ProductId.Casino] = casinoCustomer.GetSetting
            };
        }

        public IEnumerable<Product> GetPlayForFunProduct(int custId)
        {
            var playForFunProducts = repository.Fetch<Product>(new PlayForFunProductCriteria(custId));

            return playForFunProducts
                .Where(product => IsShowProduct(product.Id))
                .Select(product => product)
                .OrderBy(product => product.Id);
        }

        public IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            var criteria = new MemberRequestCriteria(param.CustId, param.ProductId);
            return repository.Fetch<MemberRequestModel>(criteria).OrderBy(member => member.Username);
        }

        public GetSettingResult GetSetting(ProductSettingParam productSettingParam)
        {
            return getSettingFuncs[productSettingParam.ProductId](productSettingParam);
        }

        public bool IsSync(int productId)
        {
            return productId != ProductId.Casino || user.GetSyncedProduct(productId);
        }

        internal bool IsShowProduct(int productId)
        {
            return productId != ProductId.FishingWorld || !user.GetHideProduct(productId);
        }
    }
}
namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequests;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Models.MemberRequests;
    using Params;

    public interface MemberRequestService
    {
        IEnumerable<Product> GetPlayForFunProduct(int custId);

        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        bool HasSetting(ProductSettingParam productSettingParam);
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly IDynamicRepository repository;
        private readonly IUser user;
        private readonly CasinoDataAccess casinoDataAccess;

        public MemberRequestServiceImpl(
            IDynamicRepository dynamicRepository,
            IUser user,
            CasinoDataAccess casinoDataAccess)
        {
            this.user = user;
            repository = dynamicRepository;
            this.casinoDataAccess = casinoDataAccess;
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

        public bool HasSetting(ProductSettingParam productSettingParam)
        {
            var hasSettingFuncs = new Dictionary<int, Func<ProductSettingParam, bool>>
            {
                [ProductId.Casino] = HasCasinoSetting
            };

            return hasSettingFuncs[productSettingParam.ProductId](productSettingParam);
        }

        public bool HasCasinoSetting(ProductSettingParam productSettingParam)
        {
            var gameKinds = casinoDataAccess.GameKinds(productSettingParam.CustId);
            var rngGame = gameKinds.FirstOrDefault(gameKind => gameKind.Id == CasinoGameKind.RNGGame);

            return rngGame != null
                && (rngGame.PositionTaking != 0 || rngGame.Commission != 0);
        }

        internal bool IsShowProduct(int productId)
        {
            return productId != ProductId.FishingWorld || !user.GetHideProduct(productId);
        }
    }
}
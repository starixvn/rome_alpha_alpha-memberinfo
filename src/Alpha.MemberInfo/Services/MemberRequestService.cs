namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequests;
    using Criteria.MemberRequests;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Models.MemberRequests;
    using Params;
    using RemotingRef;
    using Reports.DataAccesses;
    using Sunplus.Agent.Model.BL.Casino;
    using Sunplus.Agent.Model.Casino;

    public interface MemberRequestService
    {
        IEnumerable<PlayForFunProduct> SupportedProduct(int custId);

        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        IEnumerable<Result> EnableProduct(EnableProductParam param); 
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly MemberRequestDataAccess dataAccess;
        private readonly CasinoService casinoService;
        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly IUser user;

        public MemberRequestServiceImpl(
            MemberRequestDataAccess dataAccess,
            CasinoService casinoService,
            IDynamicRepository dynamicRepository,
            IUser user,
            CacheRefService cacheRefService)
        {
            this.cacheRefService = cacheRefService;
            this.dataAccess = dataAccess;
            this.casinoService = casinoService;
            this.user = user;
            repository = dynamicRepository;
        }

        public IEnumerable<PlayForFunProduct> SupportedProduct(int custId)
        {
            var playForFunProducts = ConfiguredCacheService.GetOrSet(
                $"PlayForFunProductsCacheKey-{custId}",
                () => repository.Fetch<PlayForFunProduct>(new PlayForFunProductCriteria(custId)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

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

        public IEnumerable<Result> EnableProduct(EnableProductParam param)
        {
            switch (param.ProductId)
            {
                case ProductId.Casino:
                    return EnableCasino(param);
            }

            return Enumerable.Empty<Result>();
        }

        internal IEnumerable<Result> EnableCasino(EnableProductParam param)
        {
            var results = new List<Result>();

            foreach (var member in param.Members)
            {
                var criteria = new EnableCasinoCriteria(
                                    member.CustId,
                                    member.Username,
                                    param.ActorId,
                                    param.SubAccId,
                                    param.LoginIP,
                                    param.Language);

                var updateResult = dataAccess.EnableCasino(criteria);
                results.Add(updateResult);

                if (updateResult.IsSuccessful)
                {
                    SyncCasinoStatus(member.CustId, param.LoginIP, param.ActorId, param.ActorName);
                }
            }

            return results;
        }

        internal void SyncCasinoStatus(int custId, string ip, int actorId, string actorName)
        {
            var isCustomerExist = casinoService.Customer.CheckCustSetting(custId);
            if (isCustomerExist)
            {
                casinoService.Customer.UpdateCustEnableCasino(
                    custId,
                    true,
                    false,
                    ip,
                    actorId,
                    actorName,
                    CasinoBO.AgentSystem);
            }
        }

        internal bool IsShowProduct(int productId)
        {
            return productId != ProductId.FishingWorld || !user.GetHideProduct(productId);
        }
    }
}
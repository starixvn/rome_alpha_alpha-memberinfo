namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AppResources;
    using Constants;
    using Criteria.MemberRequests;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Models.MemberRequests;
    using Params;
    using RemotingRef;
    using Sunplus.Agent.Model.BL.Casino;
    using Sunplus.Agent.Model.Casino;

    public interface MemberRequestService
    {
        IEnumerable<Product> SupportedProduct(int custId);

        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        IEnumerable<Result> EnableProduct(EnableProductParam param); 
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly CasinoService casinoService;
        private readonly IDynamicRepository repository;
        private readonly CacheRefService cacheRefService;
        private readonly IUser user;

        public MemberRequestServiceImpl(
            CasinoService casinoService,
            IDynamicRepository dynamicRepository,
            IUser user,
            CacheRefService cacheRefService)
        {
            this.cacheRefService = cacheRefService;
            this.casinoService = casinoService;
            this.user = user;
            repository = dynamicRepository;
        }

        public IEnumerable<Product> SupportedProduct(int custId)
        {
            var playForFunProducts = ConfiguredCacheService.GetOrSet(
                $"PlayForFunProductsCacheKey-{custId}",
                () => repository.Fetch<PlayForFunProduct>(new PlayForFunProductCriteria(custId)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

            return playForFunProducts
                .Where(product => IsShowProduct(product.Id))
                .Select(product => new Product(product.Id, string.Empty, cacheRefService.GetProductName))
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

        public IEnumerable<Result> EnableCasino(EnableProductParam param)
        {
            var results = new List<Result>();

            foreach (var member in param.Members)
            {
                var updateResult = ExcuteCasinoCommand(new EnableCasinoCommand(
                                        member.CustId,
                                        member.Username,
                                        param.ActorId,
                                        param.SubAccId,
                                        param.LoginIP,
                                        param.Language));

                results.Add(updateResult);

                if (updateResult.IsSuccessful)
                {
                    SyncCasinoStatus(member.CustId, param.LoginIP, param.ActorId, param.ActorName);
                }
            }

            return results;
        }

        internal Result ExcuteCasinoCommand(EnableCasinoCommand command)
        {
            repository.Execute(command);

            return new Result
                {
                    Code = command.ErrCode,
                    Message = command.ErrCode == 0
                        ? Resources.UpdatedSuccessfully
                        : $"{command.CustName}: {command.ErrMsg}"
                };
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
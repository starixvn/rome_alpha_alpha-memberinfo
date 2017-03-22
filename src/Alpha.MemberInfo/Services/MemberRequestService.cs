namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequest;
    using Criteria.PlayForFun;
    using Fanex.Caching;
    using Fanex.Common;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using RemotingRef;
    using Reports.DataAccesses;
    using Sunplus.Agent.Model.BL.Casino;
    using Sunplus.Agent.Model.Casino;

    public interface MemberRequestService
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        IEnumerable<Result> EnableProduct(EnableProductParam param);

        IEnumerable<Result> EnableCasino(EnableCasinoParam param);

        IEnumerable<PlayForFunProduct> SupportedProduct(int custId);
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
                .Select(product => product);
        }

        public bool IsShowProduct(int productId)
        {
            return productId != ProductId.FishingWorld || !user.GetHideProduct(productId);
        }

        public IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            //var criteria = new MemberRequestCriteria();

            //var memberRequestData = dataAccess.GetMemberRequest(new MemberRequestCriteria(param.CustId, param.ProductId));

            var memberRequestData = repository.Fetch<MemberRequestModel>(new MemberRequestCriteria(param.CustId, param.ProductId));

            return memberRequestData;
        }


        public IEnumerable<Result> EnableProduct(EnableProductParam param)
        {
            List<string> userInfoList = param.MembersInfo.Split(',').ToList();
            var results = new List<Result>();
            //foreach (string userInfo in userInfoList)
            //{
            //    var custId = Converter.ToInt(userInfo.Split('-')[0]);
            //    var criteria = new EnableCasinoCriteria
            //    (
            //        custId,
            //        param.ActorId,
            //        param.SubAccId,
            //        param.LoginIP,
            //        param.Language);

            //    var updateResult = dataAccess.EnableCasino(criteria);
            //    results.Add(updateResult);
            //}

            return results;
        }

        public IEnumerable<Result> EnableCasino(EnableCasinoParam param)
        {
            List<string> userInfoList = param.CustIds.Split(',').ToList();
            var results = new List<Result>();
            foreach (string userInfo in userInfoList)
            {
                var custId = Converter.ToInt(userInfo.Split('-')[0]);

                var criteria = new EnableCasinoCriteria
                (
                    custId,
                    param.ActorId,
                    param.SubAccId,
                    param.LoginIP,
                    param.Language);

                var updateResult = dataAccess.EnableCasino(criteria);
                results.Add(updateResult);

                if (updateResult.IsSuccessful)
                {
                    SyncCasinoStatus(
                        custId,
                        param.LoginIP,
                        param.ActorId,
                        param.ActorName);
                }
            }

            return results;
        }

        private void SyncCasinoStatus(
           int custId,
           string ip, 
           int actorId, 
           string actorName)
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
    }
}
namespace Alpha.MemberInfo.Services.WinLossLimit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Criteria.WinLossLimit;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using Models.WinLossLimit;
    using Params;

    public interface WinLossLimitService
    {
        IEnumerable<WinLossLimitGroupProductModel> GetWinLossLimitProducts(int custId);

        IEnumerable<CustomerInfo> GetMasterList(int custId);

        IEnumerable<CustomerInfo> GetAgentList(int custId);

        WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param);
    }

    public class WinLossLimitServiceImpl : WinLossLimitService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly WinLossLimitDataAccess winLossLimitDataAccess;
        private readonly Func<int, string> getProductName;

        public WinLossLimitServiceImpl(
            IDynamicRepository dynamicRepository,
            WinLossLimitDataAccess winLossLimitDataAccess,
            Func<int, string> getProductName)
        {
            this.dynamicRepository = dynamicRepository;
            this.winLossLimitDataAccess = winLossLimitDataAccess;
            this.getProductName = getProductName;
        }

        public IEnumerable<WinLossLimitGroupProductModel> GetWinLossLimitProducts(int custId)
        {
            var groupProducts = dynamicRepository
                .Fetch<WinLossLimitProductModel>(new GetWinLossLimitProductCriteria(custId))
                .GroupBy(product => product.Type);

            foreach (var group in groupProducts)
            {
                foreach (var product in group)
                {
                    product.ProductName = getProductName(product.ProductId);
                }

                yield return new WinLossLimitGroupProductModel(
                    group.Key == 0 ? string.Empty : getProductName(group.Key),
                    group);
            }
        }

        public IEnumerable<CustomerInfo> GetMasterList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetMasterListCriteria(custId));

        public IEnumerable<CustomerInfo> GetAgentList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetAgentListCriteria(custId));

        public WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param)
        {
            var result = new WinLossLimitPage();
            var isSabaclub = param.IsSabaclub;
            var playerId = isSabaclub && !string.IsNullOrWhiteSpace(param.PlayerId) ?
                winLossLimitDataAccess.MapPlayerIdToCustId(new MapPlayerId3rdToCustIdCriteria(param.ProductId, param.PlayerId))
                : (int?)null;

            var custWinLossLimits = winLossLimitDataAccess.GetWinLossLimits(new GetCustWinLossLimitCriteria(param, playerId));
            if (!custWinLossLimits.WinLossLimitDetail.Any())
            {
                return result;
            }
            
            var balances = dynamicRepository.Fetch<WinLossLimitDetail>(new GetBalanceCriteria(param));
            IEnumerable<WinLossLimitDetail> outstandings = Enumerable.Empty<WinLossLimitDetail>();

            foreach (var custWinLossLimit in custWinLossLimits.WinLossLimitDetail)
            {
                var balance = balances.FirstOrDefault(item => item.CustId == custWinLossLimit.CustId);
                if (balance != null)
                {
                    custWinLossLimit.CreditBalance = balance.CreditBalance;
                    custWinLossLimit.DebitBalance = balance.DebitBalance;
                    custWinLossLimit.Balance = balance.Balance;
                }

                var outstanding = outstandings.FirstOrDefault(item => item.CustId == custWinLossLimit.CustId);
                if (outstanding != null)
                {
                    custWinLossLimit.Outstanding = outstanding.Outstanding;
                }
            }

            return custWinLossLimits;
        }
    }
}

namespace Alpha.MemberInfo.Services.WinLossLimit
{
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Criteria.WinLossLimit;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Models.WinLossLimit;
    using Params;

    public interface WinLossLimitService
    {
        IEnumerable<WinLossLimitProductModel> GetWinLossLimitProducts(int custId);

        IEnumerable<CustomerInfo> GetMasterList(int custId);

        IEnumerable<CustomerInfo> GetAgentList(int custId);

        WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param);
    }

    public class WinLossLimitServiceImpl : WinLossLimitService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly WinLossLimitDataAccess winLossLimitDataAccess;

        public WinLossLimitServiceImpl(
            IDynamicRepository dynamicRepository,
            WinLossLimitDataAccess winLossLimitDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.winLossLimitDataAccess = winLossLimitDataAccess;
        }

        public IEnumerable<WinLossLimitProductModel> GetWinLossLimitProducts(int custId)
            => dynamicRepository.Fetch<WinLossLimitProductModel>(new GetWinLossLimitProductCriteria(custId));

        public IEnumerable<CustomerInfo> GetMasterList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetMasterListCriteria(custId));

        public IEnumerable<CustomerInfo> GetAgentList(int custId)
            => dynamicRepository.Fetch<CustomerInfo>(new GetAgentListCriteria(custId));

        public WinLossLimitPage GetWinLossLimitPage(WinLossLimitParam param)
        {
            var result = new WinLossLimitPage();
            var isPokerGames = param.ProductId.IsPokerGames();
            var playerId = isPokerGames && !string.IsNullOrWhiteSpace(param.PlayerId) ?
                winLossLimitDataAccess.MapPlayerIdToCustId(new MapPlayerId3rdToCustIdCriteria(param.ProductId, param.PlayerId))
                : (int?)null;

            var custWinLossLimits = winLossLimitDataAccess.GetWinLossLimits(new GetCustWinLossLimitCriteria(param, playerId));
            if (!custWinLossLimits.WinLossLimitDetail.Any())
            {
                return result;
            }

            var custIds = custWinLossLimits.WinLossLimitDetail.Select(c => c.CustId);
            var minCustId = custIds.Min();
            var maxCustId = custIds.Max();

            var balances = dynamicRepository.Fetch<WinLossLimitDetail>(new GetBalanceCriteria(param));
            IEnumerable<WinLossLimitDetail> outstandings = isPokerGames && custWinLossLimits.WinLossLimitDetail.Any()
                ? dynamicRepository.Fetch<WinLossLimitDetail>(new GetOutstandingCriteria(param, playerId, minCustId, maxCustId))
                : Enumerable.Empty<WinLossLimitDetail>();

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
